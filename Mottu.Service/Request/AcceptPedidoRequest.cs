using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class AcceptPedidoRequest : IRequest<BaseResponse>
    {
        public int IdPedido { get; set; }
        public string numeroCNH { get; set; }
    }
}
