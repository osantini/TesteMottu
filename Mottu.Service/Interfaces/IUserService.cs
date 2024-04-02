using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse> NewUser(UserRequest request);
    }
}
