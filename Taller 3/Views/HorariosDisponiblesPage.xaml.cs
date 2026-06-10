using Taller_3.ViewModels;

namespace Taller_3.Views
{
    public partial class HorariosDisponiblesPage : ContentPage
    {
        public HorariosDisponiblesPage(HorariosDisponiblesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;// Asignar el ViewModel al BindingContext
        }

        protected override async void OnAppearing()// Sobrescribir OnAppearing para cargar datos
        {
            base.OnAppearing();//ejecuta la lógica de ContentPage
            if (BindingContext is HorariosDisponiblesViewModel vm) 
            {
                await vm.LoadData();
            }
        }

        protected override void OnDisappearing() // Sobrescribir OnDisappearing para recargar datos al regresar
        {
            base.OnDisappearing();
            // Recargar datos cuando se regrese de otra página
            if (BindingContext is HorariosDisponiblesViewModel vm)
            {
                _ = vm.LoadData(); // el _ es para ignorar el warning de llamada asíncrona sin await
            }
        }
    }
}
