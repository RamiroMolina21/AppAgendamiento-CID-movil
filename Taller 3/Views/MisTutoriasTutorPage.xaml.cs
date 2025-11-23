using Taller_3.ViewModels;

namespace Taller_3.Views
{
    public partial class MisTutoriasTutorPage : ContentPage
    {
        public MisTutoriasTutorPage(MisTutoriasTutorViewModel viewModel)
        {
            InitializeComponent();  
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is MisTutoriasTutorViewModel vm)
            {
                await vm.LoadData();
            }
        }
    }
}

