using Domain.Entities;
using System.Linq;

namespace Application.Services
{
    public class ExperienciaService
    {
        private static readonly List<Experiencia> experiencias = new();

        public Experiencia Agregar(Experiencia experiencia)
        {
            experiencias.Add(experiencia);
            return experiencia;
        }

        public List<Experiencia> ObtenerTodas()
        {
            return experiencias;
        }

        public Experiencia? ObtenerPorId(int id)
        {
            return experiencias.FirstOrDefault(e => e.Id == id);
        }

        public bool Eliminar(int id)
        {
            var experiencia = experiencias.FirstOrDefault(e => e.Id == id);

            if (experiencia == null)
            {
                return false;
            }

            experiencias.Remove(experiencia);
            return true;
        }
    }
}