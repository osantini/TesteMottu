using MediatR;
using Mottu.Service.Responses;

namespace Mottu.Service.Request
{
    public class RentMotoRequest : IRequest<BaseResponse>
    {
        public string NumeroCNH { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string DataPrevisaoFim { get; set; }
    }
}
