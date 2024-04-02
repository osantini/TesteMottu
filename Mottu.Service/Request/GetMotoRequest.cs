using MediatR;
using Mottu.Service.Models;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class GetMotoRequest : IRequest<BaseResponse>
    {
        public string NomeUsuario { get; set; }
        public string Placa { get; set; }
    }
}
