using Taller_3.ViewModels;

namespace Taller_3.Views
{
    public partial class TutoriasFinalizadasTutorPage : ContentPage
    {
        public TutoriasFinalizadasTutorPage(TutoriasFinalizadasTutorViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is TutoriasFinalizadasTutorViewModel vm)
            {
                await vm.LoadData();
            }
        }
    }
}

