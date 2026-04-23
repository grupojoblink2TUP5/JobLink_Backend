public class Postulacion
{
    public int Id { get; private set; }

    public DateTime FechaPostulacion { get; private set; }
    public string Estado { get; private set; }
    public string Observaciones { get; private set; }

    public int CandidatoId { get; private set; }
    public int OfertaLaboralId { get; private set; }

    public Postulacion(int candidatoId, int ofertaLaboralId)
    {
        CandidatoId = candidatoId;
        OfertaLaboralId = ofertaLaboralId;
        FechaPostulacion = DateTime.Now;
        Estado = "Pendiente";
        Observaciones = "";
    }

    public void Aceptar()
    {
        Estado = "Aceptada";
    }

    public void Rechazar(string observacion)
    {
        Estado = "Rechazada";
        Observaciones = observacion;
    }
}