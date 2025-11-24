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
    public class MisTutoriasViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private ObservableCollection<TutoriaResponseDto> _tutorias;
        private bool _isLoading;

        public MisTutoriasViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
            _tutorias = new ObservableCollection<TutoriaResponseDto>();
            
            VerDetalleCommand = new Command<TutoriaResponseDto>(async (t) => await VerDetalle(t));
            FinalizarCommand = new Command<TutoriaResponseDto>(async (t) => await FinalizarTutoria(t));
            LoadDataCommand = new Command(async () => await LoadData());
        }

        public ObservableCollection<TutoriaResponseDto> Tutorias
        {
            get => _tutorias;
            set
            {
                _tutorias = value;
                OnPropertyChanged();
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

        public ICommand VerDetalleCommand { get; }
        public ICommand FinalizarCommand { get; }
        public ICommand LoadDataCommand { get; }

        public async Task LoadData()
        {
            IsLoading = true;
            try
            {
                var usuarioId = _authService.CurrentUser?.IdUsuario ?? 0;
                var tutorias = await _apiService.GetTutoriasByEstadoAndUsuarioAsync("Pendiente", usuarioId);
                
                Tutorias.Clear();
                foreach (var tutoria in tutorias)
                {
                    Tutorias.Add(tutoria);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar tutorías: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task VerDetalle(TutoriaResponseDto tutoria)
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
