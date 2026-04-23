namespace Domain.Entities
{
    public class Administrador : Usuario
    {
        public string NivelAcceso { get; private set; }

        public Administrador(
            string nombre,
            string apellido,
            string email,
            string contraseña,
            string nivelAcceso
        ) : base(nombre, apellido, email, contraseña)
        {
            NivelAcceso = nivelAcceso;
        }
    }
}