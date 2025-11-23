namespace Taller_3.Models
{
    public class RetroalimentacionCreateDto
    {
        public string Comentario { get; set; }
        public decimal Calificacion { get; set; }
        public int TutoriaId { get; set; }
        public int UsuarioId { get; set; }
    }
}

