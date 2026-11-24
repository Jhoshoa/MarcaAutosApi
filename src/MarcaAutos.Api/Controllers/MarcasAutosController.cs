using MarcaAutos.Application.Interfaces;
using MarcaAutos.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MarcaAutos.Api.Controllers
{
    /// <summary>
    /// Provides read-only operations for vehicle brands.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasAutosController : ControllerBase
    {
        private readonly IMarcaAutoService _marcaAutoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarcasAutosController"/> class.
        /// </summary>
        /// <param name="marcaAutoService">The service used to retrieve vehicle brands.</param>
        public MarcasAutosController(IMarcaAutoService marcaAutoService)
        {
            _marcaAutoService = marcaAutoService;            
        }

        /// <summary>
        /// Retrieves all vehicle brands.
        /// </summary>
        /// <remarks>
        /// Returns the complete collection, including brands configured by the database seed.
        /// </remarks>
        /// <returns>An HTTP 200 response containing the vehicle brands.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MarcaAutoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MarcaAutoResponse>>> GetAll()
        {
            var marcasAutos = await _marcaAutoService.ObtenerTodasLasMarcasAsync();
            var response = marcasAutos
                .Select(marca => new MarcaAutoResponse(
                    marca.Id,
                    marca.Name,
                    marca.CountryOrigin,
                    marca.FoundedYear));

            return Ok(response);
        }
    }
}
