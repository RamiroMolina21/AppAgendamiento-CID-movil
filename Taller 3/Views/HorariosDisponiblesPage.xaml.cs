using Taller_3.ViewModels;

namespace Taller_3.Views
{
    public partial class HorariosDisponiblesPage : ContentPage
    {
        public HorariosDisponiblesPage(HorariosDisponiblesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is HorariosDisponiblesViewModel vm)
            {
                await vm.LoadData();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Recargar datos cuando se regrese de otra página
            if (BindingContext is HorariosDisponiblesViewModel vm)
            {
                _ = vm.LoadData();
            }
        }
    }
}
