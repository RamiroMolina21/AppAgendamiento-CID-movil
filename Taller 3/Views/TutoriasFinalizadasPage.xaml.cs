using Taller_3.ViewModels;

namespace Taller_3.Views
{
    public partial class TutoriasFinalizadasPage : ContentPage
    {
        public TutoriasFinalizadasPage(TutoriasFinalizadasViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is TutoriasFinalizadasViewModel vm)
            {
                await vm.LoadData();
            }
        }
    }
}
