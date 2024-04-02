using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class RentMotoHandler : IRequestHandler<RentMotoRequest, BaseResponse>
    {
        private readonly IAluguelService _aluguelService;
        public RentMotoHandler(IAluguelService aluguelService)
        {
            _aluguelService = aluguelService;
        }

        public async Task<BaseResponse> Handle(RentMotoRequest request, CancellationToken cancellationToken)
        {
            var ret = await _aluguelService.RentMoto(request);
            return ret;
        }
    }
}
