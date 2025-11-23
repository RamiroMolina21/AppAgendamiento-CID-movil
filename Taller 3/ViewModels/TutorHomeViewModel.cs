using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Taller_3.Services;
using Taller_3.Views;
using Microsoft.Extensions.DependencyInjection;
using Taller_3;

namespace Taller_3.ViewModels
{
    public class TutorHomeViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        public FlyoutPage FlyoutPage { get; set; }

        public TutorHomeViewModel(AuthService authService)
        {
            _authService = authService;
            NavigateToMisTutoriasCommand = new Command(async () => await NavigateToMisTutorias());
            NavigateToTutoriasFinalizadasCommand = new Command(async () => await NavigateToTutoriasFinalizadas());
            LogoutCommand = new Command(async () => await Logout());
        }

        public string NombreCompleto => $"{_authService.CurrentUser?.Nombres} {_authService.CurrentUser?.Apellidos}";
        public string Rol => _authService.CurrentUser?.TipoRol ?? "Tutor";

        public ICommand NavigateToMisTutoriasCommand { get; }
        public ICommand NavigateToTutoriasFinalizadasCommand { get; }
        public ICommand LogoutCommand { get; }

        private async Task NavigateToMisTutorias()
        {
            if (FlyoutPage?.Detail is NavigationPage navPage)
            {
                var viewModel = MauiProgram.Services?.GetService<MisTutoriasTutorViewModel>();
                if (viewModel != null)
                {
                    await navPage.PushAsync(new MisTutoriasTutorPage(viewModel));
                    FlyoutPage.IsPresented = false;
                }
            }
        }

        private async Task NavigateToTutoriasFinalizadas()
        {
            if (FlyoutPage?.Detail is NavigationPage navPage)
            {
                var viewModel = MauiProgram.Services?.GetService<TutoriasFinalizadasTutorViewModel>();
                if (viewModel != null)
                {
                    await navPage.PushAsync(new TutoriasFinalizadasTutorPage(viewModel));
                    FlyoutPage.IsPresented = false;
                }
            }
        }

        private async Task Logout()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Cerrar Sesión", "¿Está seguro que desea cerrar sesión?", "Sí", "No");
            if (confirm)
            {
                _authService.Logout();
                var loginPage = MauiProgram.Services?.GetService<LoginPage>();
                if (loginPage != null)
                {
                    Application.Current.MainPage = loginPage;
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

