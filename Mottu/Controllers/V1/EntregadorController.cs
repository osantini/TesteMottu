using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class EntregadorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EntregadorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        BaseResponse response = new BaseResponse();

        [HttpGet("consultar-entregadores-notificados")]
        public async Task<ActionResult> GetEntregadoresNotifications(NotificationEntregadoresRequest request)
        {
            try
            {
                var entregadores = await _mediator.Send(request);
                return Ok("Ids dos entregadores notificados " + entregadores.Message);
            }
            catch 
            {
                return StatusCode(Convert.ToInt32(response.StatusCode), response.Message);
            }
        }

        [HttpPost("cadastrar-novo-entregador")]
        public async Task<ActionResult> NewEntregador([FromForm] EntregadorRequest request)
        {
            try
            {
                if (request.ImagemCNH == null || request.ImagemCNH.Length == 0)
                {
                    return BadRequest("Por favor, envie a foto da CNH.");
                }

                if (request.ImagemCNH.ContentType != "image/png" && request.ImagemCNH.ContentType != "image/bmp")
                {
                    return BadRequest("O formato do arquivo deve ser PNG ou BMP.");
                }

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

        [HttpPut("atualizar-entregador")]
        public async Task<ActionResult> UpdateEntregador([FromForm] UpdateEntregadorRequest request)
        {
            try
            {
                if (request.ImagemCNH == null || request.ImagemCNH.Length == 0)
                {
                    return BadRequest("Por favor, envie a foto da CNH.");
                }

                if (request.ImagemCNH.ContentType != "image/png" && request.ImagemCNH.ContentType != "image/bmp")
                {
                    return BadRequest("O formato do arquivo deve ser PNG ou BMP.");
                }

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

        [HttpPost("aceitar-pedido")]
        public async Task<ActionResult> AcceptPedido([FromBody] AcceptPedidoRequest request)
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

        [HttpPost("entregar-pedido")]
        public async Task<ActionResult> DeliverPedido([FromBody] DeliverPedidoRequest request)
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

    }
}
