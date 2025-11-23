using System.Net.Http.Json;
using Taller_3.Models;
using System.Net.Http;

namespace Taller_3.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://uninoculated-groved-marlena.ngrok-free.dev/api/";

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        // Auth endpoints
        public async Task<UsuarioResponseDto> LoginAsync(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/login", loginDto);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.usuario;
        }

        // Horario endpoints
        public async Task<List<HorarioResponseDto>> GetHorariosByUsuarioAsync(int usuarioId)
        {
            var response = await _httpClient.GetAsync($"Horario/usuario/{usuarioId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<HorarioResponseDto>>() ?? new List<HorarioResponseDto>();
        }

        public async Task<List<HorarioResponseDto>> GetHorariosDisponiblesAsync()
        {
            var response = await _httpClient.GetAsync("Horario/disponibles");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<HorarioResponseDto>>() ?? new List<HorarioResponseDto>();
        }

        // Tutoria endpoints
        public async Task<TutoriaResponseDto> CreateTutoriaAsync(TutoriaCreateDto tutoriaDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Tutoria", tutoriaDto);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TutoriaResponseDto>();
            return result;
        }

        public async Task<TutoriaResponseDto> GetTutoriaByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Tutoria/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TutoriaResponseDto>();
        }

        public async Task<List<TutoriaResponseDto>> GetTutoriasByEstadoAndUsuarioAsync(string estado, int usuarioId)
        {
            var response = await _httpClient.GetAsync($"Tutoria/estado/{estado}/usuario/{usuarioId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TutoriaResponseDto>>() ?? new List<TutoriaResponseDto>();
        }

        public async Task<bool> AgregarEstudiantesATutoriaAsync(int tutoriaId, AgregarEstudiantesTutoriaDto estudiantesDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"Tutoria/{tutoriaId}/estudiantes", estudiantesDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<TutoriaResponseDto> FinalizarTutoriaAsync(int id)
        {
            var response = await _httpClient.PutAsync($"Tutoria/{id}/finalizar", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TutoriaResponseDto>();
        }

        public async Task<List<UsuarioResponseDto>> GetEstudiantesByTutoriaAsync(int tutoriaId)
        {
            var response = await _httpClient.GetAsync($"Tutoria/{tutoriaId}/estudiantes");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<UsuarioResponseDto>>() ?? new List<UsuarioResponseDto>();
        }

        // Retroalimentacion endpoints
        public async Task<bool> CreateRetroalimentacionAsync(RetroalimentacionCreateDto retroalimentacionDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Retroalimentacion", retroalimentacionDto);
            return response.IsSuccessStatusCode;
        }

        // Usuario endpoints
        public async Task<List<UsuarioResponseDto>> GetUsuariosByRolAsync(string tipoRol)
        {
            var response = await _httpClient.GetAsync($"Usuario/by-rol/{tipoRol}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<UsuarioResponseDto>>() ?? new List<UsuarioResponseDto>();
        }
    }

    public class LoginResponse
    {
        public string message { get; set; }
        public UsuarioResponseDto usuario { get; set; }
    }
}
