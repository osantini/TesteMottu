using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class GetEntregadoresNotificationsHandler : IRequestHandler<NotificationEntregadoresRequest, BaseResponse>
    {
        private readonly IEntregadorService _entregadorService;
        public GetEntregadoresNotificationsHandler(IEntregadorService entregadorService)
        {
            _entregadorService = entregadorService;
        }

        public async Task<BaseResponse> Handle(NotificationEntregadoresRequest request, CancellationToken cancellationToken)
        {
            var entregadores = await _entregadorService.GetEntregadoresNotifications(request);
            return await Task.FromResult(entregadores);

        }
    }
}
