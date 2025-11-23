using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Taller_3.Services;
using Taller_3.ViewModels;
using Taller_3.Views;

namespace Taller_3
{
    public static class MauiProgram
    {
        public static IServiceProvider? Services { get; private set; }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Configurar URL de la API (debe terminar con "/")
            var apiBaseUrl = "https://uninoculated-groved-marlena.ngrok-free.dev/api/";
            // Para desarrollo local: "https://localhost:5001/api/"
            
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
                
                // Agregar headers para evitar el warning de ngrok
                client.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");
            });

            // Registrar servicios
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<AuthService>(sp => 
                new AuthService(sp.GetRequiredService<ApiService>()));

            // Registrar ViewModels
            builder.Services.AddTransient<LoginViewModel>(sp => 
                new LoginViewModel(sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<DocenteHomeViewModel>(sp => 
                new DocenteHomeViewModel(sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<HorariosDisponiblesViewModel>(sp => 
                new HorariosDisponiblesViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<AgendarTutoriaViewModel>(sp => 
                new AgendarTutoriaViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<MisTutoriasViewModel>(sp => 
                new MisTutoriasViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<DetalleTutoriaViewModel>(sp => 
                new DetalleTutoriaViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<TutoriasFinalizadasViewModel>(sp => 
                new TutoriasFinalizadasViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<CalificarTutoriaViewModel>(sp => 
                new CalificarTutoriaViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<AgregarEstudiantesViewModel>(sp => 
                new AgregarEstudiantesViewModel(sp.GetRequiredService<ApiService>()));
            
            // Registrar ViewModels del Tutor
            builder.Services.AddTransient<TutorHomeViewModel>(sp => 
                new TutorHomeViewModel(sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<MisTutoriasTutorViewModel>(sp => 
                new MisTutoriasTutorViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<DetalleTutoriaTutorViewModel>(sp => 
                new DetalleTutoriaTutorViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));
            builder.Services.AddTransient<TutoriasFinalizadasTutorViewModel>(sp => 
                new TutoriasFinalizadasTutorViewModel(sp.GetRequiredService<ApiService>(), sp.GetRequiredService<AuthService>()));

            // Registrar Views
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<DocenteHomePage>();
            builder.Services.AddTransient<HorariosDisponiblesPage>();
            builder.Services.AddTransient<AgendarTutoriaPage>();
            builder.Services.AddTransient<MisTutoriasPage>();
            builder.Services.AddTransient<DetalleTutoriaPage>();
            builder.Services.AddTransient<TutoriasFinalizadasPage>();
            builder.Services.AddTransient<DetalleTutoriaFinalizadaPage>();
            builder.Services.AddTransient<CalificarTutoriaPage>();
            builder.Services.AddTransient<AgregarEstudiantesPage>();
            
            // Registrar Views del Tutor
            builder.Services.AddTransient<TutorHomePage>();
            builder.Services.AddTransient<MisTutoriasTutorPage>();
            builder.Services.AddTransient<DetalleTutoriaTutorPage>();
            builder.Services.AddTransient<TutoriasFinalizadasTutorPage>();

            var app = builder.Build();
            Services = app.Services;
            return app;
        }
    }
}
