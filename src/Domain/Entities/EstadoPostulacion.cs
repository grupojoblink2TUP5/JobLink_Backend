namespace Domain.Entities
{
    public class EstadoPostulacion
    {
        public string Estado { get; private set; }
        public string DetalleEstado { get; private set; }
        public DateTime FechaEstado { get; private set; }

        public EstadoPostulacion(string estado, string detalle)
        {
            Estado = estado;
            DetalleEstado = detalle;
            FechaEstado = DateTime.Now;
        }
    }
}