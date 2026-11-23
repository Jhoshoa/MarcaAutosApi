using MarcaAutos.Application.Interfaces;
using MarcaAutos.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MarcaAutos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasAutosController : ControllerBase
    {
        private readonly IMarcaAutoService _marcaAutoService;

        public MarcasAutosController(IMarcaAutoService marcaAutoService)
        {
            _marcaAutoService = marcaAutoService;            
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MarcasAutos>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MarcasAutos>>> GetAll()
        {
            var marcasAutos = await _marcaAutoService.ObtenerTodasLasMarcasAsync();
            return Ok(marcasAutos);
        }
    }
}
