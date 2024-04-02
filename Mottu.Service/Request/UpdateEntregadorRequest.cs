using MediatR;
using Microsoft.AspNetCore.Http;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class UpdateEntregadorRequest : IRequest<BaseResponse>
    {
        public string NumeroCNH { get; set; }
        public IFormFile ImagemCNH { get; set; }
    }
}
