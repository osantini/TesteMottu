using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class NewMotoHandler : IRequestHandler<MotoRequest, BaseResponse>
    {
        private readonly IMotosService _motosService;
        public NewMotoHandler(IMotosService motosService)
        {
            _motosService = motosService;
        }

        public async Task<BaseResponse> Handle(MotoRequest request, CancellationToken cancellationToken)
        {
            var ret = await _motosService.NewMoto(request);
            return ret;
        }
    }
}
