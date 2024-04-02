using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mottu.Service.Models;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class MotosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MotosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("buscar-motos")]
        public async Task<ActionResult> GetMotos([FromBody] GetMotoRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);

                if (!response.IsSucess)
                {
                    return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
                }
                return StatusCode((int)response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar Motos: {ex.Message}");
            }
        }

        [HttpPost("cadastrar-nova-moto")]
        public async Task<ActionResult> NewMoto([FromBody] MotoRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);

                if (!response.IsSucess)
                {
                    return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
                }
                return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar nova moto: {ex.Message}");
            }
        }

        [HttpPut("alterar-moto")]
        public async Task<ActionResult> UpdateMoto([FromBody] UpdateMotoRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);

                if (!response.IsSucess)
                {
                    return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
                }
                return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar Moto: {ex.Message}");
            }
        }

        [HttpDelete("deletar-moto")]
        public async Task<ActionResult> DeleteMoto([FromBody] DeleteMotoRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);


                if (!response.IsSucess)
                {
                    return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
                }
                return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar moto: {ex.Message}");
            }
        }
    }
}
