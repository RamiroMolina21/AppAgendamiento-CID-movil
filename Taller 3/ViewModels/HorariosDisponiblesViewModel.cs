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
        private ObservableCollection<HorarioResponseDto> _horarios;
        private ObservableCollection<TutoriaResponseDto> _tutoriasAgendadas;
        private string _searchText;
        private bool _isLoading;

        public HorariosDisponiblesViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
            _horarios = new ObservableCollection<HorarioResponseDto>();
            _tutoriasAgendadas = new ObservableCollection<TutoriaResponseDto>();
            
            AgendarTutoriaCommand = new Command<HorarioResponseDto>(async (h) => await AgendarTutoria(h));
            VerDetalleTutoriaCommand = new Command<TutoriaResponseDto>(async (t) => await VerDetalleTutoria(t));
            FinalizarTutoriaCommand = new Command<TutoriaResponseDto>(async (t) => await FinalizarTutoria(t));
            
            LoadDataCommand = new Command(async () => await LoadData());
        }

        public ObservableCollection<HorarioResponseDto> Horarios
        {
            get => _horarios;
            set
            {
                _horarios = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TutoriaResponseDto> TutoriasAgendadas
        {
            get => _tutoriasAgendadas;
            set
            {
                _tutoriasAgendadas = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterHorarios();
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

        public int HorariosCount => Horarios.Count;
        public DateTime FechaActual => DateTime.Now;

        public ICommand AgendarTutoriaCommand { get; }
        public ICommand VerDetalleTutoriaCommand { get; }
        public ICommand FinalizarTutoriaCommand { get; }
        public ICommand LoadDataCommand { get; }

        public async Task LoadData()
        {
            IsLoading = true;
            try
            {
                var usuarioId = _authService.CurrentUser?.IdUsuario ?? 0;
                
                // Cargar horarios disponibles del docente
                var horarios = await _apiService.GetHorariosByUsuarioAsync(usuarioId);
                Horarios.Clear();
                foreach (var horario in horarios.Where(h => h.Estado?.ToLower() == "disponible"))
                {
                    Horarios.Add(horario);
                }

                // Cargar tutorías agendadas
                var tutorias = await _apiService.GetTutoriasByEstadoAndUsuarioAsync("Pendiente", usuarioId);
                TutoriasAgendadas.Clear();
                foreach (var tutoria in tutorias)
                {
                    TutoriasAgendadas.Add(tutoria);
                }

                OnPropertyChanged(nameof(HorariosCount));
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

        private void FilterHorarios()
        {
            // Implementar filtrado si es necesario
        }

        private async Task AgendarTutoria(HorarioResponseDto horario)
        {
            // Navegar usando el NavigationPage actual
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

        private async Task VerDetalleTutoria(TutoriaResponseDto tutoria)
        {
            // Navegar usando el NavigationPage actual
            if (Application.Current.MainPage is FlyoutPage flyoutPage && 
                flyoutPage.Detail is NavigationPage navPage)
            {
                var viewModel = MauiProgram.Services?.GetService<DetalleTutoriaViewModel>();
                if (viewModel != null)
                {
                    var page = new DetalleTutoriaPage(viewModel);
                    page.TutoriaId = tutoria.IdTutoria.ToString();
                    await navPage.PushAsync(page);
                    flyoutPage.IsPresented = false;
                }
            }
        }

        private async Task FinalizarTutoria(TutoriaResponseDto tutoria)
        {
            // Navegar a la página de calificar tutoría
            if (Application.Current.MainPage is FlyoutPage flyoutPage && 
                flyoutPage.Detail is NavigationPage navPage)
            {
                var viewModel = MauiProgram.Services?.GetService<CalificarTutoriaViewModel>();
                if (viewModel != null)
                {
                    var page = new CalificarTutoriaPage(viewModel);
                    page.TutoriaId = tutoria.IdTutoria.ToString();
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
