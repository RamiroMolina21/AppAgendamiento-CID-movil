using Taller_3.Models;

namespace Taller_3.Services
{
    public class AuthService
    {
        private readonly ApiService _apiService;
        private UsuarioResponseDto _currentUser;

        public AuthService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public UsuarioResponseDto CurrentUser => _currentUser;

        public bool IsAuthenticated => _currentUser != null;

        public async Task<bool> LoginAsync(string correo, string contrasena)
        {
            try
            {
                var loginDto = new LoginDto
                {
                    Correo = correo,
                    Contrasena = contrasena
                };

                _currentUser = await _apiService.LoginAsync(loginDto);
                return _currentUser != null;
            }
            catch
            {
                return false;
            }
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool IsDocente()
        {
            return _currentUser?.TipoRol?.ToLower() == "docente";
        }

        public bool IsTutor()
        {
            return _currentUser?.TipoRol?.ToLower() == "tutor";
        }
    }
}
