using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class DeleteMotoRequest : IRequest<BaseResponse>
    {
        public string Placa { get; set; }
        public string NomeUsuario { get; set; }
    }
}
