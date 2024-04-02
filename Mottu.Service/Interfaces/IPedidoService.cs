using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.Service.Interfaces
{
    public interface IPedidoService
    {
        Task<BaseResponse> NewPedido(PedidoRequest request);
    }
}
