using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Interfaces;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienciaController : ControllerBase
    {
        private readonly IExperienciaService _experienciaService;

        // Aca se maneja la inyeccion por dependencia
        public ExperienciaController(IExperienciaService experienciaService)
        {
            _experienciaService = experienciaService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_experienciaService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var experiencia = _experienciaService.GetById(id);

            if (experiencia == null)
                return NotFound();

            return Ok(experiencia);
        }


    }
}