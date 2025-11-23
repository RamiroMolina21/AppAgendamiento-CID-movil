using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Taller_3.Models;
using Taller_3.Services;

namespace Taller_3.ViewModels
{
    public class AgendarTutoriaViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private HorarioResponseDto _horarioSeleccionado;
        private string _idioma;
        private string _nivel;
        private string _tema;
        private string _modalidad;
        private bool _isLoading;

        public AgendarTutoriaViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
            GuardarCommand = new Command(async () => await GuardarTutoria(), () => !IsLoading);
        }

        public HorarioResponseDto HorarioSeleccionado
        {
            get => _horarioSeleccionado;
            set
            {
                _horarioSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public string Idioma
        {
            get => _idioma;
            set
            {
                _idioma = value;
                OnPropertyChanged();
            }
        }

        public string Nivel
        {
            get => _nivel;
            set
            {
                _nivel = value;
                OnPropertyChanged();
            }
        }

        public string Tema
        {
            get => _tema;
            set
            {
                _tema = value;
                OnPropertyChanged();
            }
        }

        public string Modalidad
        {
            get => _modalidad;
            set
            {
                _modalidad = value;
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

        public List<string> Modalidades { get; } = new List<string> { "Presencial", "Virtual" };

        public async Task LoadHorario(int horarioId)
        {
            try
            {
                var horarios = await _apiService.GetHorariosByUsuarioAsync(_authService.CurrentUser.IdUsuario);
                HorarioSeleccionado = horarios.FirstOrDefault(h => h.IdHorario == horarioId);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar horario: {ex.Message}", "OK");
            }
        }

        private async Task GuardarTutoria()
        {
            if (string.IsNullOrWhiteSpace(Idioma) || 
                string.IsNullOrWhiteSpace(Nivel) || 
                string.IsNullOrWhiteSpace(Tema) || 
                string.IsNullOrWhiteSpace(Modalidad) ||
                HorarioSeleccionado == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            IsLoading = true;
            try
            {
                var tutoriaDto = new TutoriaCreateDto
                {
                    Idioma = Idioma,
                    Nivel = Nivel,
                    Tema = Tema,
                    Modalidad = Modalidad,
                    Estado = "Pendiente",
                    FechaTutoria = HorarioSeleccionado.FechaInicio,
                    UsuarioId = _authService.CurrentUser.IdUsuario,
                    HorarioId = HorarioSeleccionado.IdHorario
                };

                await _apiService.CreateTutoriaAsync(tutoriaDto);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Tutoría agendada correctamente", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al agendar tutoría: {ex.Message}", "OK");
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
