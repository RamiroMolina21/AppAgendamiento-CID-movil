using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Taller_3.Services;
using Taller_3.Views;
using Microsoft.Extensions.DependencyInjection;
using Taller_3;

namespace Taller_3.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        private string _correo;
        private string _contrasena;
        private bool _isLoading;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new Command(async () => await LoginAsync(), () => !IsLoading);
        }

        public string Correo
        {
            get => _correo;
            set
            {
                _correo = value;
                OnPropertyChanged();
            }
        }

        public string Contrasena
        {
            get => _contrasena;
            set
            {
                _contrasena = value;
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
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public ICommand LoginCommand { get; }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            IsLoading = true;
            try
            {
                var success = await _authService.LoginAsync(Correo, Contrasena);
                if (success)
                {
                    if (_authService.IsDocente())
                    {
                        // Cambiar MainPage directamente a DocenteHomePage (FlyoutPage)
                        var docenteHomePage = MauiProgram.Services?.GetService<DocenteHomePage>();
                        if (docenteHomePage != null)
                        {
                            Application.Current.MainPage = docenteHomePage;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar la página principal", "OK");
                        }
                    }
                    else if (_authService.IsTutor())
                    {
                        // Cambiar MainPage directamente a TutorHomePage (FlyoutPage)
                        var tutorHomePage = MauiProgram.Services?.GetService<TutorHomePage>();
                        if (tutorHomePage != null)
                        {
                            Application.Current.MainPage = tutorHomePage;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar la página principal", "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Rol no reconocido", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Credenciales inválidas", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al iniciar sesión: {ex.Message}", "OK");
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
