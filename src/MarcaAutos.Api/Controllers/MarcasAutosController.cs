using MarcaAutos.Api.Dtos;
using MarcaAutos.Application.Interfaces;
using MarcaAutos.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarcaAutos.Api.Controllers
{
    /// <summary>
    /// Provides operations for vehicle brands.
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
        public async Task<ActionResult<IEnumerable<MarcaAutoResponse>>> GetAll(
            CancellationToken cancellationToken
        )
        {
            var marcasAutos = await _marcaAutoService.ObtenerTodasLasMarcasAsync(cancellationToken);
            var response = marcasAutos.Select(marca => new MarcaAutoResponse(
                marca.Id,
                marca.Name,
                marca.CountryOrigin,
                marca.FoundedYear
            ));

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MarcaAutoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MarcaAutoResponse>> Create(
            [FromBody] CreateMarcaAutoRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateMarcaAutoCommand(
                request.Name,
                request.CountryOrigin,
                request.FoundedYear
            );
            var result = await _marcaAutoService.CrearMarcaAutoAsync(command, cancellationToken);

            var response = new MarcaAutoResponse(
                result.Id,
                result.Name,
                result.CountryOrigin,
                result.FoundedYear
            );

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MarcaAutoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MarcaAutoResponse>> GetById(
            int id,
            CancellationToken cancellationToken
        )
        {
            var result = await _marcaAutoService.ObtenerMarcaAutoPorIdAsync(id, cancellationToken);

            if (result is null)
            {
                return NotFound();
            }

            var response = new MarcaAutoResponse(
                result.Id,
                result.Name,
                result.CountryOrigin,
                result.FoundedYear
            );

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteById(int id, CancellationToken cancellationToken)
        {
            await _marcaAutoService.BorrarMarcaAutoAsync(id, cancellationToken);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<MarcaAutoResponse>> Edit(
            int id,
            [FromBody] CreateMarcaAutoRequest data,
            CancellationToken cancellationToken
        )
        {
            var marcaAuto = new CreateMarcaAutoCommand(
                data.Name,
                data.CountryOrigin,
                data.FoundedYear
            );

            var result = await _marcaAutoService.EditarMarcaAutoAsync(
                id,
                marcaAuto,
                cancellationToken
            );

            if (result is null)
            {
                return NotFound();
            }

            var response = new MarcaAutoResponse(
                result.Id,
                result.Name,
                result.CountryOrigin,
                result.FoundedYear
            );

            return Ok(response);
        }
    }
}
