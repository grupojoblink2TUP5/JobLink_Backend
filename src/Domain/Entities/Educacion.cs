namespace Domain.Entities
{
    public class Educacion
    {
        public int Id { get; private set; }

        public string Institucion { get; private set; }
        public string Titulo { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }

        // Relación con Candidato
        public int CandidatoId { get; private set; }

        public Educacion(
            string institucion,
            string titulo,
            DateTime fechaInicio,
            DateTime fechaFin,
            int candidatoId
        )
        {
            if (fechaFin < fechaInicio)
                throw new Exception("La fecha de fin no puede ser menor a la de inicio");

            Institucion = institucion;
            Titulo = titulo;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            CandidatoId = candidatoId;
        }

        public void ActualizarEducacion(string institucion, string titulo)
        {
            Institucion = institucion;
            Titulo = titulo;
        }
    }
}