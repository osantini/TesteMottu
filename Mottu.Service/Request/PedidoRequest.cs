using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class PedidoRequest : IRequest<BaseResponse>
    {
        public decimal ValorCorrida { get; set; }
        public string Situacao { get; set; }
        public string NomeUsuario { get; set; }
    }
}
