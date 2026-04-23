namespace Domain.Entities
{
    public class Candidato : Usuario
    {
        public string Dni { get; private set; }
        public string Telefono { get; private set; }
        public string Direccion { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public string DescripcionPerfil { get; private set; }

        public Candidato(
            string nombre,
            string apellido,
            string email,
            string contraseña,
            string dni,
            string telefono,
            string direccion,
            DateTime fechaNacimiento,
            string descripcionPerfil
        ) : base(nombre, apellido, email, contraseña)
        {
            Dni = dni;
            Telefono = telefono;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
            DescripcionPerfil = descripcionPerfil;
        }
    }
}