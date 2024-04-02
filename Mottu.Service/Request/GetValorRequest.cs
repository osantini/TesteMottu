using MediatR;
using Mottu.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Service.Request
{
    public class GetValorRequest : IRequest<BaseResponse>
    {
        public string NumeroCNH { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string DataPrevisaoFim { get; set; }
    }
}
