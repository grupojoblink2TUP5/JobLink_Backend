namespace Domain.Entities
{
    public class Cv
    {
        public int Id { get; private set; }

        public string ArchivoUrl { get; private set; }

        // Relación con Candidato
        public int CandidatoId { get; private set; }

        public Cv(string archivoUrl, int candidatoId)
        {
            ArchivoUrl = archivoUrl;
            CandidatoId = candidatoId;
        }

        public void ActualizarCv(string nuevaUrl)
        {
            ArchivoUrl = nuevaUrl;
        }
    }
}