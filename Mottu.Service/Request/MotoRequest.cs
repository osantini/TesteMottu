using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class MotoRequest : IRequest<BaseResponse>
    {
        public string Ano { get; set; }
        public string Modelo { get; set; }     
        public string Placa { get; set; }
        public string Status { get; set; }
        public int EntregadorId { get; set; }
        public string NomeUsuario { get; set; }
    }
}
