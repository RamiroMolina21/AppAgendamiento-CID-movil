namespace Taller_3.Models
{
    public class HorarioResponseDto
    {
        public int IdHorario { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string Cupos { get; set; }
        public string Espacio { get; set; }
        public string Estado { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellidos { get; set; }
        public string UsuarioCorreo { get; set; }
    }
}
 