using Mottu.Service.Interfaces;

namespace Mottu.Service.Services
{
    public class PermissionService
    {
        private readonly IApplicationContext _context;

        public PermissionService(IApplicationContext context)
        {
            _context = context;
        }

        public bool CheckPermission(string userName)
        {
            try
            {
                var user = _context.Usuarios.FirstOrDefault(p => p.Nome == userName);
                if (user.Perfil == "admin")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
