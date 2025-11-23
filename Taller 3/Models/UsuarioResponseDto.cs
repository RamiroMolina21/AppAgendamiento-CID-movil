namespace Taller_3.Models
{
    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string TipoRol { get; set; }
    }
}
