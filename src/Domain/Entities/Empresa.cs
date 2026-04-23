namespace Domain.Entities
{
    public class Empresa
    {
        public int Id { get; private set; }

        public string RazonSocial { get; private set; }
        public string Cuit { get; private set; }
        public string Rubro { get; private set; }
        public string Descripcion { get; private set; }
        public string SitioWeb { get; private set; }
        public string Ubicacion { get; private set; }
        public bool Aprobada { get; private set; }

        public Empresa(
            string razonSocial,
            string cuit,
            string rubro,
            string descripcion,
            string sitioWeb,
            string ubicacion
        )
        {
            RazonSocial = razonSocial;
            Cuit = cuit;
            Rubro = rubro;
            Descripcion = descripcion;
            SitioWeb = sitioWeb;
            Ubicacion = ubicacion;
            Aprobada = false; // por defecto no aprobada
        }

        public void Aprobar()
        {
            Aprobada = true;
        }

        public void Rechazar()
        {
            Aprobada = false;
        }

        public void ActualizarDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}