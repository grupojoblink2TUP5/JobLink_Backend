namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set;}
        public string Apellido { get; private set;}
        public string Email { get; private set;}
        public string Contraseña { get; private set;}
        public DateTime FechaRegistro { get; private set;}
        public string Estado { get; private set;}

        public Usuario(string nombre, string apellido, string email, string contraseña)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contraseña = contraseña;
            FechaRegistro = DateTime.Now;
            Estado = "Activo";
        }

        public void CambiarEstado(string nuevoEstado)
        {
            Estado = nuevoEstado;
        }

        public void CambiarEmail(string nuevoEmail)
        {
            Email = nuevoEmail;
        }
    }
}

