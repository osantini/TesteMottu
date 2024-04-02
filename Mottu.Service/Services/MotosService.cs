using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mottu.Infra;
using Mottu.Service.Interfaces;
using Mottu.Service.Models;
using Mottu.Service.Request;
using Mottu.Service.Responses;
using Newtonsoft.Json;

namespace Mottu.Service.Services
{
    public class MotosService : IMotosService
    {
        private readonly AppSettings _settings;
        private readonly ILogger<IMotosService> _logger;
        private readonly IApplicationContext _context;
        BaseResponse ret = new BaseResponse();

        public MotosService(IOptions<AppSettings> settings, ILogger<IMotosService> logger, IApplicationContext context)
        {
            _settings = settings.Value;
            _logger = logger;
            _context = context;
        }

        public async Task<BaseResponse> GetMotos(GetMotoRequest request)
        {
            try
            {
                PermissionService permissionService = new PermissionService(_context);

                bool userAdmin = permissionService.CheckPermission(request.NomeUsuario);

                if (userAdmin)
                {
                    var query = _context.Motos.AsQueryable();
                    query = FilterService.FilterMotos(query, request);
                    string jsonResult = JsonConvert.SerializeObject(query.ToList());
                    ret.StatusCode = System.Net.HttpStatusCode.OK;
                    ret.IsSucess = true;
                    ret.Message = jsonResult;
                    return ret;
                }

                ret.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                ret.IsSucess = false;
                ret.Message = "Usuário não autorizado.";
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BaseResponse> NewMoto(MotoRequest request)
        {
            try
            {
                PermissionService permissionService = new PermissionService(_context);

                bool userAdmin = permissionService.CheckPermission(request.NomeUsuario);

                if (userAdmin)
                {
                    var newMoto = new Motos
                    {
                        Ano = request.Ano,
                        Modelo = request.Modelo,
                        Placa = request.Placa,
                        Status = request.Status,
                        EntregadorId = request.EntregadorId
                    };

                    _context.Motos.Add(newMoto);
                    await _context.SaveChangesAsync();

                    ret.StatusCode = System.Net.HttpStatusCode.OK;
                    ret.IsSucess = true;
                    ret.Message = "Nova moto cadastrada com sucesso.";
                    return ret;
                }
                ret.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                ret.IsSucess = false;
                ret.Message = "Usuário não autorizado.";
                return ret;
            }
            catch
            {
                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                return ret;
            }
        }

        public async Task<BaseResponse> UpdateMoto(UpdateMotoRequest request)
        {
            try
            {
                PermissionService permissionService = new PermissionService(_context);

                bool userAdmin = permissionService.CheckPermission(request.NomeUsuario);

                if (userAdmin)
                {
                    var currentData = _context.Motos.FirstOrDefault(p => p.Placa == request.PlacaAtual);

                    if (currentData != null)
                    {
                        if (!String.IsNullOrEmpty(request.PlacaNova))
                            currentData.Placa = request.PlacaNova;

                        await _context.SaveChangesAsync();
                    }
                    ret.StatusCode = System.Net.HttpStatusCode.OK;
                    ret.IsSucess = true;
                    ret.Message = "Moto alterada com sucesso.";
                    return ret;
                }
                ret.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                ret.IsSucess = false;
                ret.Message = "Usuário não autorizado.";
                return ret;
            }
            catch
            {
                return ret;
            }
        }

        public async Task<BaseResponse> DeleteMoto(DeleteMotoRequest request)
        {
            try
            {
                PermissionService permissionService = new PermissionService(_context);

                bool userAdmin = permissionService.CheckPermission(request.NomeUsuario);

                if (userAdmin)
                {
                    var delete = _context.Motos.Where(m => m.Placa == request.Placa).FirstOrDefault();

                    if (delete != null)
                    {
                        var aluguelMotos = _context.AluguelMotos.Where(a => a.MotoId == delete.Id && a.Status == "Ativo").FirstOrDefault();

                        if (aluguelMotos == null)
                        {
                            _context.Motos.Remove(delete);
                            await _context.SaveChangesAsync();

                            ret.StatusCode = System.Net.HttpStatusCode.OK;
                            ret.IsSucess = true;
                            ret.Message = "Moto deletada com sucesso.";
                            return ret;
                        }
                        ret.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                        ret.IsSucess = false;
                        ret.Message = "Moto consta aluguel ativo";
                        return ret;
                    }
                }
                ret.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                ret.IsSucess = false;
                ret.Message = "Usuário não autorizado.";
                return ret;
            }
            catch
            {
                return ret;
            }
        }
    }
}
