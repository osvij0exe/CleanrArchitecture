using CleanArchitecture.Application.Alquileres.GetAlquiler;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Alquileres
{
    [ApiController]
    [Route("api/alquileres")]
    public class AlquileresController: ControllerBase
    {
        private readonly ISender _sender;

        public AlquileresController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlquilerById(Guid id, CancellationToken cancellationToken = default!)
        {

            var query = new GetAlquilerQuery(id);

            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.IsSuccess ? Ok(resultado.Value) : NotFound(); 
        }

        [HttpPost]
        public async Task<IActionResult> ReservaAlquiler(Guid id, AlquilerReservaRequest request, CancellationToken cancellationToken = default!)
        {

            var command = new ReservarAlquilerCommand(
                VehiculoId: request.VehiculoId,
                UserId: request.UserId,
                FechaInicio: request.StartDate,
                FechaFin: request.EndDate);

            var resultado = await _sender.Send(command, cancellationToken);

            if(resultado.IsFailure)
            {
                return BadRequest(resultado.Error);
            }

            return CreatedAtAction(nameof(GetAlquilerById),new {id = resultado.Value}, resultado.Value);



        }



    }

}
