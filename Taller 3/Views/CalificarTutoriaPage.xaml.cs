using Taller_3.ViewModels;

namespace Taller_3.Views
{
    [QueryProperty(nameof(TutoriaId), "TutoriaId")]
    public partial class CalificarTutoriaPage : ContentPage
    {
        public string TutoriaId { get; set; }

        public CalificarTutoriaPage(CalificarTutoriaViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is CalificarTutoriaViewModel vm && !string.IsNullOrEmpty(TutoriaId))
            {
                if (int.TryParse(TutoriaId, out int tutoriaId))
                {
                    await vm.LoadTutoria(tutoriaId);
                }
            }
        }
    }
}

