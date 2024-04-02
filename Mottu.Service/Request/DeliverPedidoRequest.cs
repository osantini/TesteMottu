using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class DeliverPedidoRequest : IRequest<BaseResponse>
    {
        public int IdPedido { get; set; }
        public string numeroCNH { get; set; }
    }
}
