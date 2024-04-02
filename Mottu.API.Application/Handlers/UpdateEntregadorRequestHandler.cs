using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class UpdateEntregadorRequestHandler : IRequestHandler<UpdateEntregadorRequest, BaseResponse>
    {
        private readonly IEntregadorService _entregadorService;
        public UpdateEntregadorRequestHandler(IEntregadorService entregadorService)
        {
            _entregadorService = entregadorService;
        }
        public async Task<BaseResponse> Handle(UpdateEntregadorRequest request, CancellationToken cancellationToken)
        {
            var ret = await _entregadorService.UpdateEntregador(request);
            return ret;

        }
    }
}
