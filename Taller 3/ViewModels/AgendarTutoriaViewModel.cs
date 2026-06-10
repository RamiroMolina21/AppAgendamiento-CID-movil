using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Taller_3.Models;
using Taller_3.Services;

namespace Taller_3.ViewModels
{
    public class AgendarTutoriaViewModel : INotifyPropertyChanged
    {
        private const int MaxEstudiantes = 5;
        private const int TotalPasos = 3;

        private readonly ApiService _apiService;
        private readonly AuthService _authService;
        private HorarioResponseDto _horarioSeleccionado;
        private string _idioma;
        private string _nivel;
        private string _tema;
        private UsuarioResponseDto _tutorSeleccionado;
        private List<UsuarioResponseDto> _tutores = new();
        private readonly List<EstudianteSeleccionable> _estudiantesCompletos = new();
        private ObservableCollection<EstudianteSeleccionable> _estudiantesDisponibles = new();
        private ObservableCollection<ModalidadChipItem> _modalidadesChips = new();
        private string _busquedaEstudiante;
        private int _pasoActual = 1;
        private bool _isLoading;

        public AgendarTutoriaViewModel(ApiService apiService, AuthService authService)
        {
            _apiService = apiService;
            _authService = authService;

            ModalidadesChips = new ObservableCollection<ModalidadChipItem>
            {
                new ModalidadChipItem("Virtual"),
                new ModalidadChipItem("Presencial"),
                new ModalidadChipItem("Híbrido")
            };

            ContinuarCommand = new Command(async () => await AvanzarPaso(), () => !IsLoading);
            AtrasCommand = new Command(RetrocederPaso, () => !IsLoading && PasoActual > 1);
            CancelarCommand = new Command(async () => await Cancelar());
            ToggleEstudianteCommand = new Command<EstudianteSeleccionable>(ToggleEstudiante);
            ToggleModalidadCommand = new Command<ModalidadChipItem>(ToggleModalidad);
            LoadTutoresCommand = new Command(async () => await LoadTutores());
        }

        public HorarioResponseDto HorarioSeleccionado
        {
            get => _horarioSeleccionado;
            set
            {
                _horarioSeleccionado = value;
                OnPropertyChanged();
                ActualizarFechaHoraDesdeHorario();
            }
        }

        public string Idioma
        {
            get => _idioma;
            set { _idioma = value; OnPropertyChanged(); }
        }

        public string Nivel
        {
            get => _nivel;
            set { _nivel = value; OnPropertyChanged(); }
        }

        public string Tema
        {
            get => _tema;
            set { _tema = value; OnPropertyChanged(); }
        }

        public string FechaTexto =>
            HorarioSeleccionado?.FechaInicio.ToString("dd/MM/yyyy") ?? "—";

        public string HoraTexto =>
            HorarioSeleccionado == null
                ? "—"
                : $"{HorarioSeleccionado.HoraInicio:HH:mm} - {HorarioSeleccionado.HoraFin:HH:mm}";

        public string BusquedaEstudiante
        {
            get => _busquedaEstudiante;
            set
            {
                _busquedaEstudiante = value;
                OnPropertyChanged();
                AplicarFiltroEstudiantes();
            }
        }

        public UsuarioResponseDto TutorSeleccionado
        {
            get => _tutorSeleccionado;
            set { _tutorSeleccionado = value; OnPropertyChanged(); }
        }

        public List<UsuarioResponseDto> Tutores
        {
            get => _tutores;
            set { _tutores = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ModalidadChipItem> ModalidadesChips
        {
            get => _modalidadesChips;
            set { _modalidadesChips = value; OnPropertyChanged(); }
        }

        public ObservableCollection<EstudianteSeleccionable> EstudiantesDisponibles
        {
            get => _estudiantesDisponibles;
            set
            {
                _estudiantesDisponibles = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EstudiantesSeleccionadosTexto));
            }
        }

        public int PasoActual
        {
            get => _pasoActual;
            set
            {
                if (_pasoActual == value)
                    return;

                _pasoActual = value;
                NotificarCambioPaso();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                ((Command)ContinuarCommand).ChangeCanExecute();
                ((Command)AtrasCommand).ChangeCanExecute();
            }
        }

        public bool EsPaso1 => PasoActual == 1;
        public bool EsPaso2 => PasoActual == 2;
        public bool EsPaso3 => PasoActual == 3;
        public bool MostrarBotonAtras => PasoActual > 1;
        public bool Linea1Activa => PasoActual >= 2;
        public bool Linea2Activa => PasoActual >= 3;
        public bool Paso1Activo => PasoActual == 1;
        public bool Paso1Completado => PasoActual > 1;
        public bool Paso2Activo => PasoActual == 2;
        public bool Paso2Completado => PasoActual > 2;
        public bool Paso3Activo => PasoActual == 3;

        public string TituloPaso =>
            PasoActual switch
            {
                1 => "Tema de la tutoría",
                2 => "Asignación",
                3 => "Estudiantes",
                _ => "Agendar Tutoría"
            };

        public string TextoBotonPrincipal =>
            PasoActual == TotalPasos ? "Guardar ✓" : "Continuar →";

        public int EstudiantesSeleccionadosCount =>
            _estudiantesCompletos.Count(e => e.IsSeleccionado);

        public string EstudiantesSeleccionadosTexto =>
            $"{EstudiantesSeleccionadosCount} / {MaxEstudiantes}";

        public ICommand ContinuarCommand { get; }
        public ICommand AtrasCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand ToggleEstudianteCommand { get; }
        public ICommand ToggleModalidadCommand { get; }
        public ICommand LoadTutoresCommand { get; }

        public async Task LoadTutores()
        {
            try
            {
                IsLoading = true;
                var tutores = await _apiService.GetUsuariosByRolAsync("Tutor");
                var docentes = await _apiService.GetUsuariosByRolAsync("Docente");

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

        public async Task LoadEstudiantes()
        {
            try
            {
                IsLoading = true;
                var estudiantes = await _apiService.GetUsuariosByRolAsync("Estudiante");
                _estudiantesCompletos.Clear();
                foreach (var estudiante in estudiantes)
                {
                    _estudiantesCompletos.Add(new EstudianteSeleccionable
                    {
                        Estudiante = estudiante,
                        IsSeleccionado = false
                    });
                }
                AplicarFiltroEstudiantes();
                OnPropertyChanged(nameof(EstudiantesSeleccionadosTexto));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar estudiantes: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ActualizarFechaHoraDesdeHorario()
        {
            OnPropertyChanged(nameof(FechaTexto));
            OnPropertyChanged(nameof(HoraTexto));
        }

        private void AplicarFiltroEstudiantes()
        {
            IEnumerable<EstudianteSeleccionable> filtrados = _estudiantesCompletos;

            if (!string.IsNullOrWhiteSpace(BusquedaEstudiante))
            {
                var busqueda = BusquedaEstudiante.Trim().ToLowerInvariant();
                filtrados = _estudiantesCompletos.Where(e =>
                    (e.Estudiante?.NombreCompleto?.ToLowerInvariant().Contains(busqueda) ?? false) ||
                    (e.Estudiante?.Nombres?.ToLowerInvariant().Contains(busqueda) ?? false) ||
                    (e.Estudiante?.Apellidos?.ToLowerInvariant().Contains(busqueda) ?? false) ||
                    (e.Estudiante?.Correo?.ToLowerInvariant().Contains(busqueda) ?? false));
            }

            EstudiantesDisponibles.Clear();
            foreach (var estudiante in filtrados)
            {
                EstudiantesDisponibles.Add(estudiante);
            }
        }

        private void ToggleModalidad(ModalidadChipItem chip)
        {
            if (chip == null)
                return;

            chip.IsSeleccionado = !chip.IsSeleccionado;
        }

        private void ToggleEstudiante(EstudianteSeleccionable estudianteSeleccionable)
        {
            if (estudianteSeleccionable == null)
                return;

            if (!estudianteSeleccionable.IsSeleccionado &&
                EstudiantesSeleccionadosCount >= MaxEstudiantes)
            {
                Application.Current.MainPage.DisplayAlert(
                    "Cupos completos",
                    $"Solo puede agregar hasta {MaxEstudiantes} estudiantes por tutoría.",
                    "OK");
                return;
            }

            estudianteSeleccionable.IsSeleccionado = !estudianteSeleccionable.IsSeleccionado;
            OnPropertyChanged(nameof(EstudiantesSeleccionadosCount));
            OnPropertyChanged(nameof(EstudiantesSeleccionadosTexto));
        }

        private async Task AvanzarPaso()
        {
            if (!await ValidarPasoActual())
                return;

            if (PasoActual < TotalPasos)
            {
                PasoActual++;
                return;
            }

            await GuardarTutoria();
        }

        private void RetrocederPaso()
        {
            if (PasoActual > 1)
                PasoActual--;
        }

        private async Task Cancelar()
        {
            var confirmar = await Application.Current.MainPage.DisplayAlert(
                "Cancelar",
                "¿Desea cancelar el agendamiento de la tutoría?",
                "Sí",
                "No");

            if (!confirmar)
                return;

            if (Application.Current.MainPage is FlyoutPage flyoutPage &&
                flyoutPage.Detail is NavigationPage navPage)
            {
                await navPage.PopAsync();
            }
        }

        private async Task<bool> ValidarPasoActual()
        {
            switch (PasoActual)
            {
                case 1:
                    if (string.IsNullOrWhiteSpace(Idioma) ||
                        string.IsNullOrWhiteSpace(Nivel) ||
                        string.IsNullOrWhiteSpace(Tema))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Complete idioma, nivel y tema.", "OK");
                        return false;
                    }

                    if (!ModalidadesChips.Any(m => m.IsSeleccionado))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Seleccione al menos una modalidad.", "OK");
                        return false;
                    }

                    return true;

                case 2:
                    if (TutorSeleccionado == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Seleccione un tutor o docente.", "OK");
                        return false;
                    }

                    if (HorarioSeleccionado == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "No se encontró el horario seleccionado.", "OK");
                        return false;
                    }

                    return true;

                default:
                    return true;
            }
        }

        private DateTime? ObtenerFechaTutoriaDesdeHorario()
        {
            if (HorarioSeleccionado == null)
                return null;

            return HorarioSeleccionado.FechaInicio.Date
                .Add(HorarioSeleccionado.HoraInicio.TimeOfDay);
        }

        private string ObtenerModalidadSeleccionada()
        {
            return string.Join(", ", ModalidadesChips.Where(m => m.IsSeleccionado).Select(m => m.Nombre));
        }

        private async Task GuardarTutoria()
        {
            var fechaTutoria = ObtenerFechaTutoriaDesdeHorario();
            if (fechaTutoria == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No se pudo obtener la fecha y hora del horario seleccionado.",
                    "OK");
                return;
            }

            IsLoading = true;
            try
            {
                var tutoriaDto = new TutoriaCreateDto
                {
                    Idioma = Idioma.Trim(),
                    Nivel = Nivel.Trim(),
                    Tema = Tema.Trim(),
                    Modalidad = ObtenerModalidadSeleccionada(),
                    Estado = "Pendiente",
                    FechaTutoria = fechaTutoria.Value,
                    UsuarioId = TutorSeleccionado.IdUsuario,
                    HorarioId = HorarioSeleccionado.IdHorario,
                    TutorId = TutorSeleccionado.IdUsuario
                };

                var tutoriaCreada = await _apiService.CreateTutoriaAsync(tutoriaDto);
                var estudiantesSeleccionados = _estudiantesCompletos
                    .Where(e => e.IsSeleccionado)
                    .ToList();

                if (estudiantesSeleccionados.Count > 0 && tutoriaCreada != null)
                {
                    var estudiantesDto = new AgregarEstudiantesTutoriaDto
                    {
                        EstudianteIds = estudiantesSeleccionados
                            .Select(e => e.Estudiante.IdUsuario)
                            .ToList()
                    };

                    var agregados = await _apiService.AgregarEstudiantesATutoriaAsync(
                        tutoriaCreada.IdTutoria,
                        estudiantesDto);

                    if (!agregados)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "Aviso",
                            "La tutoría se creó, pero no se pudieron agregar los estudiantes. Puede agregarlos desde el detalle.",
                            "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "Éxito",
                            "Tutoría agendada y estudiantes agregados correctamente",
                            "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Tutoría agendada correctamente", "OK");
                }

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

        private void NotificarCambioPaso()
        {
            OnPropertyChanged(nameof(PasoActual));
            OnPropertyChanged(nameof(EsPaso1));
            OnPropertyChanged(nameof(EsPaso2));
            OnPropertyChanged(nameof(EsPaso3));
            OnPropertyChanged(nameof(MostrarBotonAtras));
            OnPropertyChanged(nameof(Linea1Activa));
            OnPropertyChanged(nameof(Linea2Activa));
            OnPropertyChanged(nameof(Paso1Activo));
            OnPropertyChanged(nameof(Paso1Completado));
            OnPropertyChanged(nameof(Paso2Activo));
            OnPropertyChanged(nameof(Paso2Completado));
            OnPropertyChanged(nameof(Paso3Activo));
            OnPropertyChanged(nameof(TituloPaso));
            OnPropertyChanged(nameof(TextoBotonPrincipal));
            ((Command)AtrasCommand).ChangeCanExecute();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
