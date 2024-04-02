using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PedidoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        BaseResponse response = new BaseResponse();

        [HttpPost("cadastrar-novo-pedido")]
        public async Task<ActionResult> NewPedido([FromBody] PedidoRequest request)
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
            catch
            {
                return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);

            }
        }
    }
}
