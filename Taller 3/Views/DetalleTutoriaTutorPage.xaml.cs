using Taller_3.ViewModels;

namespace Taller_3.Views
{
    [QueryProperty(nameof(TutoriaId), "TutoriaId")]
    public partial class DetalleTutoriaTutorPage : ContentPage
    {
        public string TutoriaId { get; set; }

        public DetalleTutoriaTutorPage(DetalleTutoriaTutorViewModel viewModel)
        {
            InitializeComponent(); 
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is DetalleTutoriaTutorViewModel vm && !string.IsNullOrEmpty(TutoriaId))
            {
                if (int.TryParse(TutoriaId, out int tutoriaId))
                {
                    await vm.LoadTutoria(tutoriaId);
                }
            }
        }
    }
}

