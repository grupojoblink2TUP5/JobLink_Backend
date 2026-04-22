namespace Domain.Entities
{
    public class Experiencia
    {
        public int Id { get; set; }
        public required string Empresa { get; set; }
        public required string Puesto { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public required string Descripcion { get; set; }
    }
}