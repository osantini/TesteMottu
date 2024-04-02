using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.Service.Interfaces
{
    public interface IAluguelService
    {
        Task<BaseResponse> GetValor(GetValorRequest request);
        Task<BaseResponse> RentMoto(RentMotoRequest request);
    }
}
