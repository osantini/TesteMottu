using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class UserRequest : IRequest<BaseResponse>
    {
        public string Nome { get; set; }
        public string Perfil { get; set; }
    }
}
