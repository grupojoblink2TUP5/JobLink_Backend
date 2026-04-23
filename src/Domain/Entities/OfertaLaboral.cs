namespace Domain.Entities
{
    public class OfertaLaboral
    {
        public int Id { get; private set; }

        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public double Salario { get; private set; }
        public string TipoOferta { get; private set; }
        public string Ubicacion { get; private set; }
        public DateTime FechaPublicacion { get; private set; }
        public DateTime FechaCierre { get; private set; }
        public string Estado { get; private set; }

        // Relación con Empresa
        public int EmpresaId { get; private set; }

        public OfertaLaboral(
            string titulo,
            string descripcion,
            double salario,
            string tipoOferta,
            string ubicacion,
            DateTime fechaCierre,
            int empresaId
        )
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Salario = salario;
            TipoOferta = tipoOferta;
            Ubicacion = ubicacion;
            FechaPublicacion = DateTime.Now;
            FechaCierre = fechaCierre;
            Estado = "Activa";
            EmpresaId = empresaId;
        }

        public void CerrarOferta()
        {
            Estado = "Cerrada";
        }

        public void ActualizarSalario(double nuevoSalario)
        {
            if (nuevoSalario <= 0)
                throw new Exception("El salario debe ser mayor a 0");

            Salario = nuevoSalario;
        }

        public void ActualizarDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}