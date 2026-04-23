namespace Domain.Entities
{
    public class Notificacion
    {
        public int Id { get; private set; }

        public string Mensaje { get; private set; }
        public DateTime FechaHora { get; private set; }
        public bool Leida { get; private set; }

        // Relación
        public int UsuarioId { get; private set; }

        public Notificacion(string mensaje, int usuarioId)
        {
            Mensaje = mensaje;
            UsuarioId = usuarioId;
            FechaHora = DateTime.Now;
            Leida = false;
        }

        public void MarcarComoLeida()
        {
            Leida = true;
        }
    }
}