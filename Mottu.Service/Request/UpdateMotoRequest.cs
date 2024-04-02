using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class UpdateMotoRequest : IRequest<BaseResponse>
    {
        public string PlacaAtual { get; set; }
        public string PlacaNova { get; set; }
        public string NomeUsuario { get; set; }
    }
}
