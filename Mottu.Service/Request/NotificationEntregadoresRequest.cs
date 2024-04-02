using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class NotificationEntregadoresRequest : IRequest<BaseResponse>
    {
        public string NomeUsuario { get; set; }
        public int IdPedido { get; set; }
    }
}
