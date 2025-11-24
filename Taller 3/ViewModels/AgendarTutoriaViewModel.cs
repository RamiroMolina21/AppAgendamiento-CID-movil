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
        private UsuarioResponseDto _tutorSeleccionado;
        private List<UsuarioResponseDto> _tutores;
        private bool _isLoading;

        public AgendarTutoriaViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
            Tutores = new List<UsuarioResponseDto>();
            GuardarCommand = new Command(async () => await GuardarTutoria(), () => !IsLoading);
            LoadTutoresCommand = new Command(async () => await LoadTutores());
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

        public UsuarioResponseDto TutorSeleccionado
        {
            get => _tutorSeleccionado;
            set
            {
                _tutorSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public List<UsuarioResponseDto> Tutores
        {
            get => _tutores;
            set
            {
                _tutores = value;
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
        public ICommand LoadTutoresCommand { get; }

        public List<string> Modalidades { get; } = new List<string> { "Presencial", "Virtual" };

        public async Task LoadTutores()
        {
            try
            {
                IsLoading = true;
                var tutores = await _apiService.GetUsuariosByRolAsync("Tutor");
                var docentes = await _apiService.GetUsuariosByRolAsync("Docente");
                
                // Combinar tutores y docentes
                var todosUsuarios = new List<UsuarioResponseDto>();
                todosUsuarios.AddRange(tutores);
                todosUsuarios.AddRange(docentes);
                
                Tutores = todosUsuarios;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar tutores y docentes: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

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
                HorarioSeleccionado == null ||
                TutorSeleccionado == null)
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
                    HorarioId = HorarioSeleccionado.IdHorario,
                    TutorId = TutorSeleccionado.IdUsuario
                };

                await _apiService.CreateTutoriaAsync(tutoriaDto);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Tutoría agendada correctamente", "OK");
                
                // Volver a la página anterior
                if (Application.Current.MainPage is FlyoutPage flyoutPage && 
                    flyoutPage.Detail is NavigationPage navPage)
                {
                    await navPage.PopAsync();
                }
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
