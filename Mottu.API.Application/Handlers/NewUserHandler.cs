using MediatR;
using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.API.Application.Handlers
{
    public class NewUserHandler : IRequestHandler<UserRequest, BaseResponse>
    {
        private readonly IUserService _userService;
        public NewUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            var ret = await _userService.NewUser(request);
            return ret;
        }
    }
}
