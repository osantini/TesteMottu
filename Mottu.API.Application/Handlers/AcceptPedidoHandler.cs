using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class AcceptPedidoHandler : IRequestHandler<AcceptPedidoRequest, BaseResponse>
    {
        private readonly IEntregadorService _entregadorService;
        public AcceptPedidoHandler(IEntregadorService entregadorService)
        {
            _entregadorService = entregadorService;
        }

        public async Task<BaseResponse> Handle(AcceptPedidoRequest request, CancellationToken cancellationToken)
        {
            var ret = await _entregadorService.AcceptPedido(request);
            return ret;
        }
    }
}
