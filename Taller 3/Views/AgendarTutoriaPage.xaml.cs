using Taller_3.ViewModels;

namespace Taller_3.Views
{
    [QueryProperty(nameof(HorarioId), "HorarioId")]
    public partial class AgendarTutoriaPage : ContentPage
    {
        public string HorarioId { get; set; }

        public AgendarTutoriaPage(AgendarTutoriaViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is AgendarTutoriaViewModel vm && !string.IsNullOrEmpty(HorarioId))
            {
                if (int.TryParse(HorarioId, out int horarioId))
                {
                    await vm.LoadHorario(horarioId);
                }
            }
        }
    }
}
