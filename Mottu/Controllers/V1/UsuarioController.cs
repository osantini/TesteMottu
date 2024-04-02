using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mottu.Service.Request;

namespace Mottu.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("cadastrar-novo-usuario")]
        public async Task<ActionResult> NewUser([FromBody] UserRequest request)
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
                return StatusCode(500, $"Erro ao salvar novo usuário: {ex.Message}");
            }
        }
    }
}
