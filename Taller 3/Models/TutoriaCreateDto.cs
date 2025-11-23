namespace Taller_3.Models
{
    public class TutoriaCreateDto
    {
        public string Idioma { get; set; }
        public string Nivel { get; set; }
        public string Tema { get; set; }
        public string Modalidad { get; set; }
        public string Estado { get; set; }
        public DateTime FechaTutoria { get; set; }
        public int UsuarioId { get; set; }
        public int HorarioId { get; set; }
    }
}
 