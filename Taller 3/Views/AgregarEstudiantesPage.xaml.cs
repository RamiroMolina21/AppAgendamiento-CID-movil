using Taller_3.ViewModels;

namespace Taller_3.Views
{
    [QueryProperty(nameof(TutoriaId), "TutoriaId")]//QueryProperty Atributo que permite recibir parámetros por query string en la navegación.
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
            if (BindingContext is AgregarEstudiantesViewModel vm && !string.IsNullOrEmpty(TutoriaId))// Verifica que el BindingContext sea del tipo esperado y que TutoriaId no esté vacío.
            {
                if (int.TryParse(TutoriaId, out int tutoriaId))//out int recibe el valor parseado.
                {
                    vm.TutoriaId = tutoriaId;
                    await vm.LoadEstudiantes();
                }
            }
        }
    }
}

