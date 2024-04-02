using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class NewPedidoHandler : IRequestHandler<PedidoRequest, BaseResponse>
    {
        private readonly IPedidoService _pedidoService;
        public NewPedidoHandler(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task<BaseResponse> Handle(PedidoRequest request, CancellationToken cancellationToken)
        {
            var ret = await _pedidoService.NewPedido(request);
            return ret;
        }
    }
}
