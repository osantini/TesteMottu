using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class UpdateMotoHandler : IRequestHandler<UpdateMotoRequest, BaseResponse>
    {
        private readonly IMotosService _motosService;
        public UpdateMotoHandler(IMotosService motosService)
        {
            _motosService = motosService;
        }
        public async Task<BaseResponse> Handle(UpdateMotoRequest request, CancellationToken cancellationToken)
        {
            var ret = await _motosService.UpdateMoto(request);
            return ret;

        }
    }
}
