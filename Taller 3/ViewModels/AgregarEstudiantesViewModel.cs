using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Taller_3.Models;
using Taller_3.Services;

namespace Taller_3.ViewModels
{
    public class EstudianteSeleccionable : INotifyPropertyChanged
    {
        public UsuarioResponseDto Estudiante { get; set; }
        private bool _isSeleccionado;

        public bool IsSeleccionado
        {
            get => _isSeleccionado;
            set
            {
                _isSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AgregarEstudiantesViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private ObservableCollection<EstudianteSeleccionable> _estudiantesDisponibles;
        private bool _isLoading;

        public AgregarEstudiantesViewModel(ApiService apiService)
        {
            _apiService = apiService;
            _estudiantesDisponibles = new ObservableCollection<EstudianteSeleccionable>();
            
            ToggleEstudianteCommand = new Command<EstudianteSeleccionable>(ToggleEstudiante);
            GuardarCommand = new Command(async () => await Guardar(), () => !IsLoading && _estudiantesDisponibles.Any(e => e.IsSeleccionado));
        }

        public int TutoriaId { get; set; }
        public ObservableCollection<EstudianteSeleccionable> EstudiantesDisponibles
        {
            get => _estudiantesDisponibles;
            set
            {
                _estudiantesDisponibles = value;
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

        public ICommand ToggleEstudianteCommand { get; }
        public ICommand GuardarCommand { get; }

        public async Task LoadEstudiantes()
        {
            IsLoading = true;
            try
            {
                var estudiantes = await _apiService.GetUsuariosByRolAsync("Estudiante");
                EstudiantesDisponibles.Clear();
                foreach (var estudiante in estudiantes)
                {
                    EstudiantesDisponibles.Add(new EstudianteSeleccionable { Estudiante = estudiante, IsSeleccionado = false });
                }
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

        private void ToggleEstudiante(EstudianteSeleccionable estudianteSeleccionable)
        {
            if (estudianteSeleccionable != null)
            {
                estudianteSeleccionable.IsSeleccionado = !estudianteSeleccionable.IsSeleccionado;
                ((Command)GuardarCommand).ChangeCanExecute();
            }
        }

        private async Task Guardar()
        {
            var estudiantesSeleccionados = _estudiantesDisponibles.Where(e => e.IsSeleccionado).ToList();
            
            if (estudiantesSeleccionados.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Seleccione al menos un estudiante", "OK");
                return;
            }

            IsLoading = true;
            try
            {
                var estudiantesDto = new AgregarEstudiantesTutoriaDto
                {
                    EstudianteIds = estudiantesSeleccionados.Select(e => e.Estudiante.IdUsuario).ToList()
                };

                var success = await _apiService.AgregarEstudiantesATutoriaAsync(TutoriaId, estudiantesDto);
                if (success)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Estudiantes agregados correctamente", "OK");
                    
                    // Volver a la página anterior
                    if (Application.Current.MainPage is FlyoutPage flyoutPage && 
                        flyoutPage.Detail is NavigationPage navPage)
                    {
                        await navPage.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudieron agregar los estudiantes", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al agregar estudiantes: {ex.Message}", "OK");
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

