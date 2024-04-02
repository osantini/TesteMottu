using MediatR;
using Microsoft.AspNetCore.Http;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class EntregadorRequest : IRequest<BaseResponse>
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string DataNascimento { get; set; }
        public string NumeroCNH { get; set; }
        public string TipoCNH { get; set; }
        public IFormFile ImagemCNH { get; set; }
    }
}
