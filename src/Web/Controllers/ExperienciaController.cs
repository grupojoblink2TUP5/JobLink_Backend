using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienciaController : ControllerBase
    {
        private readonly ExperienciaService _service;

        public ExperienciaController()
        {
            _service = new ExperienciaService();
        }

        [HttpGet]
        public ActionResult<List<Experiencia>> Get()
        {
            return _service.ObtenerTodas();
        }

        [HttpPost]
        public ActionResult Post(Experiencia experiencia)
        {
            var result = _service.Agregar(experiencia);
            return CreatedAtAction(nameof(Get),result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var eliminado = _service.Eliminar(id);

            if (!eliminado)
            {
                return NotFound($"No se encontró una experiencia con id {id}");
            }

            return NoContent();
        }
    }
}