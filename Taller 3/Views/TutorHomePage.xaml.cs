using Taller_3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Taller_3;

namespace Taller_3.Views
{
    public partial class TutorHomePage : FlyoutPage
    {
        public TutorHomePage(TutorHomeViewModel viewModel)
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
                var misTutoriasViewModel = MauiProgram.Services?.GetService<MisTutoriasTutorViewModel>();
                if (misTutoriasViewModel != null)
                {
                    Detail = new NavigationPage(new MisTutoriasTutorPage(misTutoriasViewModel));
                }
            }
        }
    }
}

