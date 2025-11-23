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
    public class DetalleTutoriaTutorViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private TutoriaResponseDto _tutoria;
        private ObservableCollection<UsuarioResponseDto> _estudiantes;
        private bool _isLoading;

        public DetalleTutoriaTutorViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
            _estudiantes = new ObservableCollection<UsuarioResponseDto>();
            
            CalificarCommand = new Command(async () => await CalificarTutoria());
        }

        public TutoriaResponseDto Tutoria
        {
            get => _tutoria;
            set
            {
                _tutoria = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UsuarioResponseDto> Estudiantes
        {
            get => _estudiantes;
            set
            {
                _estudiantes = value;
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

        public ICommand CalificarCommand { get; }

        public async Task LoadTutoria(int tutoriaId)
        {
            IsLoading = true;
            try
            {
                Tutoria = await _apiService.GetTutoriaByIdAsync(tutoriaId);
                await LoadEstudiantes(tutoriaId);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar tutoría: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadEstudiantes(int tutoriaId)
        {
            try
            {
                var estudiantes = await _apiService.GetEstudiantesByTutoriaAsync(tutoriaId);
                Estudiantes.Clear();
                foreach (var estudiante in estudiantes)
                {
                    Estudiantes.Add(estudiante);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar estudiantes: {ex.Message}", "OK");
            }
        }

        private async Task CalificarTutoria()
        {
            if (Tutoria == null)
                return;

            // Navegar a la página de calificar tutoría
            if (Application.Current.MainPage is FlyoutPage flyoutPage && 
                flyoutPage.Detail is NavigationPage navPage)
            {
                var viewModel = MauiProgram.Services?.GetService<CalificarTutoriaViewModel>();
                if (viewModel != null)
                {
                    var page = new CalificarTutoriaPage(viewModel);
                    page.TutoriaId = Tutoria.IdTutoria.ToString();
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

