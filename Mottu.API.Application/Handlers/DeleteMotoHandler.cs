using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class DeleteMotoHandler : IRequestHandler<DeleteMotoRequest, BaseResponse>
    {
        private readonly IMotosService _motosService;
        public DeleteMotoHandler(IMotosService motosService)
        {
            _motosService = motosService;
        }

        public async Task<BaseResponse> Handle(DeleteMotoRequest request, CancellationToken cancellationToken)
        {
            var ret = await _motosService.DeleteMoto(request);
            return ret;

        }
    }
}
