using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class DeliverPedidoHandler : IRequestHandler<DeliverPedidoRequest, BaseResponse>
    {
        private readonly IEntregadorService _entregadorService;
        public DeliverPedidoHandler(IEntregadorService entregadorService)
        {
            _entregadorService = entregadorService;
        }

        public async Task<BaseResponse> Handle(DeliverPedidoRequest request, CancellationToken cancellationToken)
        {
            var ret = await _entregadorService.DeliverPedido(request);
            return ret;
        }
    }
}
