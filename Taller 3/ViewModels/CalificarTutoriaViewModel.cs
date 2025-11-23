using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Taller_3.Models;
using Taller_3.Services;

namespace Taller_3.ViewModels
{
    public class CalificarTutoriaViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private TutoriaResponseDto _tutoria;
        private decimal _calificacion = 3.0m;
        private string _comentario;
        private bool _isLoading;

        public CalificarTutoriaViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
            GuardarCommand = new Command(async () => await GuardarRetroalimentacion(), () => !IsLoading);
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

        public decimal Calificacion
        {
            get => _calificacion;
            set
            {
                _calificacion = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CalificacionTexto));
            }
        }

        public string CalificacionTexto => $"Calificación: {Calificacion:F1}";

        public string Comentario
        {
            get => _comentario;
            set
            {
                _comentario = value;
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
                ((Command)GuardarCommand).ChangeCanExecute();
            }
        }

        public ICommand GuardarCommand { get; }

        public async Task LoadTutoria(int tutoriaId)
        {
            try
            {
                Tutoria = await _apiService.GetTutoriaByIdAsync(tutoriaId);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar tutoría: {ex.Message}", "OK");
            }
        }

        private async Task GuardarRetroalimentacion()
        {
            if (Tutoria == null || _authService.CurrentUser == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Datos incompletos", "OK");
                return;
            }

            if (Calificacion < 1 || Calificacion > 5)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La calificación debe estar entre 1 y 5", "OK");
                return;
            }

            IsLoading = true;
            try
            {
                var retroalimentacionDto = new RetroalimentacionCreateDto
                {
                    Comentario = Comentario ?? string.Empty,
                    Calificacion = Calificacion,
                    TutoriaId = Tutoria.IdTutoria,
                    UsuarioId = _authService.CurrentUser.IdUsuario
                };

                var success = await _apiService.CreateRetroalimentacionAsync(retroalimentacionDto);
                
                if (success)
                {
                    // Finalizar la tutoría después de crear la retroalimentación
                    await _apiService.FinalizarTutoriaAsync(Tutoria.IdTutoria);
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Retroalimentación guardada y tutoría finalizada", "OK");
                    
                    // Volver a la página anterior
                    if (Application.Current.MainPage is FlyoutPage flyoutPage && 
                        flyoutPage.Detail is NavigationPage navPage)
                    {
                        await navPage.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo guardar la retroalimentación", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al guardar: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

