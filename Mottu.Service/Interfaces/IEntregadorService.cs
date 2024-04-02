using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.Service.Interfaces
{
    public interface IEntregadorService
    {
        Task<BaseResponse> NewEntregador(EntregadorRequest request);
        Task<BaseResponse> UpdateEntregador(UpdateEntregadorRequest request);
        Task<BaseResponse> GetEntregadoresNotifications(NotificationEntregadoresRequest request);
        Task<BaseResponse> AcceptPedido(AcceptPedidoRequest request);
        Task<BaseResponse> DeliverPedido(DeliverPedidoRequest request);
    }
}

