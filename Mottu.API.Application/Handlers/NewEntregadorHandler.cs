using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class NewEntregadorHandler : IRequestHandler<EntregadorRequest, BaseResponse>
    {
        private readonly IEntregadorService _entregadorService;
        public NewEntregadorHandler(IEntregadorService entregadorService)
        {
            _entregadorService = entregadorService;
        }

        public async Task<BaseResponse> Handle(EntregadorRequest request, CancellationToken cancellationToken)
        {
            var ret = await _entregadorService.NewEntregador(request);
            return ret;
        }
    }
}
