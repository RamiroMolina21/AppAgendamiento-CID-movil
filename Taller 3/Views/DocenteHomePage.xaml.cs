using Taller_3.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Taller_3.Views
{
    public partial class DocenteHomePage : FlyoutPage
    {
        public DocenteHomePage(DocenteHomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.FlyoutPage = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            // Establecer la página inicial en el Detail si aún no está establecida
            if (Detail == null || (Detail is NavigationPage navPage && navPage.CurrentPage == null))
            {
                var horariosViewModel = MauiProgram.Services?.GetService<HorariosDisponiblesViewModel>();
                if (horariosViewModel != null)
                {
                    Detail = new NavigationPage(new HorariosDisponiblesPage(horariosViewModel));
                }
            }
        }
    }
}
