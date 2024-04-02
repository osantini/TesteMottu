using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mottu.Infra;
using Mottu.Service.Interfaces;
using Mottu.Service.Models;
using Mottu.Service.Request;
using Mottu.Service.Responses;

namespace Mottu.Service.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _settings;
        private readonly ILogger<IUserService> _logger;
        private readonly IApplicationContext _context;

        public UserService(IOptions<AppSettings> settings, ILogger<IUserService> logger, IApplicationContext context)
        {
            _settings = settings.Value;
            _logger = logger;
            _context = context;
        }

        BaseResponse ret = new BaseResponse();

        public async Task<BaseResponse> NewUser(UserRequest request)
        {
            try
            {
                {
                    var newUser = new Usuarios
                    {
                        Nome = request.Nome,
                        Perfil = request.Perfil
                    };

                    _context.Usuarios.Add(newUser);
                    await _context.SaveChangesAsync();
                }
                ret.StatusCode = System.Net.HttpStatusCode.OK;
                ret.IsSucess = true;
                ret.Message = "Usuario cadastrado com sucesso.";
                return ret;
            }
            catch
            {
                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                return ret;
            }
        }
    }
}
