using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Taller_3.Models;
using Taller_3.Services;
using Taller_3.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Taller_3.ViewModels
{
    public class HorariosDisponiblesViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private readonly List<HorarioResponseDto> _horariosOriginales = new();
        private ObservableCollection<HorarioGrupoItem> _gruposHorarios = new();
        private string _searchText;
        private bool _isLoading;

        public HorariosDisponiblesViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;

            AgendarTutoriaCommand = new Command<HorarioResponseDto>(async (h) => await AgendarTutoria(h));
            ToggleExpandCommand = new Command<HorarioGrupoItem>(ToggleExpand);
            LoadDataCommand = new Command(async () => await LoadData());
        }

        public ObservableCollection<HorarioGrupoItem> GruposHorarios
        {
            get => _gruposHorarios;
            set
            {
                _gruposHorarios = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HorariosCount));
                OnPropertyChanged(nameof(TotalFechasCount));
                OnPropertyChanged(nameof(ContadorTexto));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                AplicarFiltroYAgrupacion();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public int HorariosCount => GruposHorarios.Count;
        public int TotalFechasCount => GruposHorarios.Sum(g => g.CantidadOcurrencias);
        public string ContadorTexto =>
            TotalFechasCount > HorariosCount
                ? $"{HorariosCount} ({TotalFechasCount} fechas)"
                : HorariosCount.ToString();
        public DateTime FechaActual => DateTime.Now;

        public ICommand AgendarTutoriaCommand { get; }
        public ICommand ToggleExpandCommand { get; }
        public ICommand LoadDataCommand { get; }

        public async Task LoadData()
        {
            IsLoading = true;
            try
            {
                var usuarioId = _authService.CurrentUser?.IdUsuario ?? 0;
                var horarios = await _apiService.GetHorariosByUsuarioAsync(usuarioId);

                _horariosOriginales.Clear();
                _horariosOriginales.AddRange(
                    horarios.Where(h => h.Estado?.ToLower() == "disponible"));

                AplicarFiltroYAgrupacion();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void AplicarFiltroYAgrupacion()
        {
            IEnumerable<HorarioResponseDto> filtrados = _horariosOriginales;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var busqueda = SearchText.Trim().ToLowerInvariant();
                filtrados = _horariosOriginales.Where(h =>
                    (h.Titulo?.ToLowerInvariant().Contains(busqueda) ?? false) ||
                    (h.Espacio?.ToLowerInvariant().Contains(busqueda) ?? false) ||
                    h.FechaInicio.ToString("dd/MM/yyyy").Contains(busqueda));
            }

            var grupos = AgruparHorarios(filtrados.ToList());

            GruposHorarios.Clear();
            foreach (var grupo in grupos)
            {
                GruposHorarios.Add(grupo);
            }

            OnPropertyChanged(nameof(HorariosCount));
            OnPropertyChanged(nameof(TotalFechasCount));
            OnPropertyChanged(nameof(ContadorTexto));
        }

        private static List<HorarioGrupoItem> AgruparHorarios(List<HorarioResponseDto> horarios)
        {
            return horarios
                .GroupBy(h => new
                {
                    Titulo = (h.Titulo ?? string.Empty).Trim().ToLowerInvariant(),
                    HoraInicio = h.HoraInicio.TimeOfDay,
                    HoraFin = h.HoraFin.TimeOfDay,
                    Espacio = (h.Espacio ?? string.Empty).Trim().ToLowerInvariant()
                })
                .Select(g =>
                {
                    var referencia = g.OrderBy(h => h.FechaInicio).First();
                    return new HorarioGrupoItem(
                        referencia.Titulo,
                        referencia.Espacio,
                        referencia.HoraInicio,
                        referencia.HoraFin,
                        g);
                })
                .OrderBy(g => g.ProximaFecha)
                .ThenBy(g => g.HoraInicio)
                .ToList();
        }

        private void ToggleExpand(HorarioGrupoItem grupo)
        {
            if (grupo == null)
                return;

            var expandir = !grupo.IsExpanded;

            foreach (var item in GruposHorarios)
            {
                item.IsExpanded = item == grupo && expandir;
            }
        }

        private async Task AgendarTutoria(HorarioResponseDto horario)
        {
            if (Application.Current.MainPage is FlyoutPage flyoutPage &&
                flyoutPage.Detail is NavigationPage navPage)
            {
                var viewModel = MauiProgram.Services?.GetService<AgendarTutoriaViewModel>();
                if (viewModel != null)
                {
                    var page = new AgendarTutoriaPage(viewModel);
                    page.HorarioId = horario.IdHorario.ToString();
                    await navPage.PushAsync(page);
                    flyoutPage.IsPresented = false;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
