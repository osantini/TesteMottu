using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Models;
using Mottu.Service.Request;
using Mottu.Service.Responses;
using Mottu.Service.Services;

namespace Mottu.API.Application.Handlers
{
    public class GetMotosHandler : IRequestHandler<GetMotoRequest, BaseResponse>
    {
        private readonly IMotosService _motosService;
        public GetMotosHandler(IMotosService motosService)
        {
            _motosService = motosService;
        }

        public async Task<BaseResponse> Handle(GetMotoRequest? request, CancellationToken cancellationToken)
        {
            var ret = await _motosService.GetMotos(request);
            return ret;

        }
    }
}
