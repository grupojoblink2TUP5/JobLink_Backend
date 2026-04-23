using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienciaController : ControllerBase
    {
        private readonly ExperienciaService _experienciaService;

        public ExperienciaController()
        {
            _experienciaService = new ExperienciaService();
        }

        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId([FromRoute] int id)
        {
            var experiencia = _experienciaService.ObtenerPorId(id);

            if (experiencia == null)
                return NotFound();

            return Ok(experiencia);
        }
        [HttpPost]
        public IActionResult Post(Experiencia experiencia)
        {
            var result = _experienciaService.Agregar(experiencia);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = result.Id }, result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var eliminado = _experienciaService.Eliminar(id);

            if (!eliminado)
            {
                return NotFound($"No se encontró una experiencia con id {id}");
            }

            return NoContent();
        }
    }
}