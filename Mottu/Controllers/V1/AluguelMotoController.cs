using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AluguelMotoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AluguelMotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        BaseResponse response = new BaseResponse();

        [HttpGet("consulta-valor")]
        public async Task<ActionResult> GetValor([FromBody] GetValorRequest request)
        {
            try
            {
                response = await _mediator.Send(request);

                if (!response.IsSucess)
                {
                    return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
                }
                return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
            }
        }

        [HttpPost("alugar-moto")]
        public async Task<ActionResult> RentMoto([FromBody] RentMotoRequest request)
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
                return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
            }
        }

    }
}
