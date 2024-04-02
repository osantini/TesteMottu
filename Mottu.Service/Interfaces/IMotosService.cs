using Mottu.Service.Models;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.Service.Interfaces
{
    public interface IMotosService
    {
        Task<BaseResponse> GetMotos(GetMotoRequest request);
        Task<BaseResponse> NewMoto(MotoRequest request);
        Task<BaseResponse> UpdateMoto(UpdateMotoRequest request);
        Task<BaseResponse> DeleteMoto(DeleteMotoRequest request);
    }
}
