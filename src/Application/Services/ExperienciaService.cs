using Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class ExperienciaService : IExperienciaService
    {
        private readonly List<Experiencia> _experiencias;

        public ExperienciaService()
        {
            // Datos de ejemplo
            _experiencias = new List<Experiencia>
            {
                new Experiencia { Id = 1, Empresa = "Siclair", Puesto = "Desarrollador", fechaInicio = new DateTime(2020, 1, 1), fechaFin = new DateTime(2021, 12, 31), Descripcion = "desarrollo de aplicaciones web" },
                new Experiencia { Id = 2, Empresa = "Globant", Puesto = "Analista", fechaInicio = new DateTime(2019, 5, 1), fechaFin = null, Descripcion = "análisis de datos y generación de informes." }
            };
        }

        public List<Experiencia> GetAll()
        {
            return _experiencias;
        }

        public Experiencia? GetById(int id)
        {
            return _experiencias.FirstOrDefault(e => e.Id == id);
        }
    }
}
