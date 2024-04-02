using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class GetValorHandler : IRequestHandler<GetValorRequest, BaseResponse>
    {
        private readonly IAluguelService _aluguelService;
        public GetValorHandler(IAluguelService aluguelService)
        {
            _aluguelService = aluguelService;
        }

        public async Task<BaseResponse> Handle(GetValorRequest request, CancellationToken cancellationToken)
        {
            var ret = await _aluguelService.GetValor(request);
            return ret;
        }
    }
}
