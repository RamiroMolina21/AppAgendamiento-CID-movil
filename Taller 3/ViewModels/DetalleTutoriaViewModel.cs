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
    public class DetalleTutoriaViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private TutoriaResponseDto _tutoria;
        private ObservableCollection<UsuarioResponseDto> _estudiantes;
        private bool _isLoading;

        public DetalleTutoriaViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
            _estudiantes = new ObservableCollection<UsuarioResponseDto>();
            
            AgregarEstudiantesCommand = new Command(async () => await AgregarEstudiantes());
            LoadDataCommand = new Command(async () => await LoadData());
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

        public int CuposDisponibles => Tutoria != null ? 5 - Estudiantes.Count : 0;
        public string CuposTexto => Tutoria != null ? $"{Estudiantes.Count}/5 disponibles" : "0/5 disponibles";

        public ICommand AgregarEstudiantesCommand { get; }
        public ICommand LoadDataCommand { get; }

        public async Task LoadTutoria(int tutoriaId)
        {
            IsLoading = true;
            try
            {
                Tutoria = await _apiService.GetTutoriaByIdAsync(tutoriaId);
                await LoadEstudiantes(tutoriaId);
                OnPropertyChanged(nameof(CuposDisponibles));
                OnPropertyChanged(nameof(CuposTexto));
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
                OnPropertyChanged(nameof(CuposDisponibles));
                OnPropertyChanged(nameof(CuposTexto));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar estudiantes: {ex.Message}", "OK");
            }
        }

        private async Task AgregarEstudiantes()
        {
            // Navegar a la página de agregar estudiantes
            if (Application.Current.MainPage is FlyoutPage flyoutPage && 
                flyoutPage.Detail is NavigationPage navPage && 
                Tutoria != null)
            {
                var viewModel = MauiProgram.Services?.GetService<AgregarEstudiantesViewModel>();
                if (viewModel != null)
                {
                    var page = new AgregarEstudiantesPage(viewModel);
                    page.TutoriaId = Tutoria.IdTutoria.ToString();
                    await navPage.PushAsync(page);
                    flyoutPage.IsPresented = false;
                    
                    // Recargar estudiantes cuando se regrese de la página
                    page.Disappearing += async (s, e) =>
                    {
                        if (Tutoria != null)
                        {
                            await LoadEstudiantes(Tutoria.IdTutoria);
                        }
                    };
                }
            }
        }

        private async Task LoadData()
        {
            // Implementar si es necesario
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
