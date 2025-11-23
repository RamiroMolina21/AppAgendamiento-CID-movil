using Taller_3.ViewModels;

namespace Taller_3.Views
{
    [QueryProperty(nameof(TutoriaId), "TutoriaId")]
    public partial class AgregarEstudiantesPage : ContentPage
    {
        public string TutoriaId { get; set; }

        public AgregarEstudiantesPage(AgregarEstudiantesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is AgregarEstudiantesViewModel vm && !string.IsNullOrEmpty(TutoriaId))
            {
                if (int.TryParse(TutoriaId, out int tutoriaId))
                {
                    vm.TutoriaId = tutoriaId;
                    await vm.LoadEstudiantes();
                }
            }
        }
    }
}

