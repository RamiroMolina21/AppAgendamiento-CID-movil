using Taller_3.Views;

namespace Taller_3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Configurar rutas
            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute("DocenteHome", typeof(DocenteHomePage));
            Routing.RegisterRoute("HorariosDisponibles", typeof(HorariosDisponiblesPage));
            Routing.RegisterRoute("AgendarTutoria", typeof(AgendarTutoriaPage));
            Routing.RegisterRoute("MisTutorias", typeof(MisTutoriasPage));
            Routing.RegisterRoute("DetalleTutoria", typeof(DetalleTutoriaPage));
            Routing.RegisterRoute("TutoriasFinalizadas", typeof(TutoriasFinalizadasPage));
            Routing.RegisterRoute("DetalleTutoriaFinalizada", typeof(DetalleTutoriaFinalizadaPage));

            MainPage = new AppShell();
            
            // Navegar inicialmente al login
            Shell.Current.GoToAsync("//Login");
        }
    }
}
