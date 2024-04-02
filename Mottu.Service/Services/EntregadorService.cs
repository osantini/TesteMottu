using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mottu.Infra;
using Mottu.Service.Interfaces;
using Mottu.Service.Models;
using Mottu.Service.Request;
using Mottu.Service.Responses;
using Newtonsoft.Json;
using System.Globalization;

namespace Mottu.Service.Services
{
    public class EntregadorService : IEntregadorService
    {
        private readonly AppSettings _settings;
        private readonly ILogger<IEntregadorService> _logger;
        private readonly IApplicationContext _context;
        BaseResponse ret = new BaseResponse();

        public EntregadorService(IOptions<AppSettings> settings, ILogger<IEntregadorService> logger, IApplicationContext context)
        {
            _settings = settings.Value;
            _logger = logger;
            _context = context;
        }

        public async Task<BaseResponse> NewEntregador(EntregadorRequest request)
        {
            try
            {
                {
                    DateTime dataNascimento = Convert.ToDateTime(request.DataNascimento).ToUniversalTime();
                    var cnh = _context.Entregadores.FirstOrDefault(e => e.NumeroCNH == request.NumeroCNH);

                    if (cnh != null)
                    {
                        ret.StatusCode = System.Net.HttpStatusCode.Conflict;
                        ret.IsSucess = false;
                        ret.Message = "Numero CNH ja existe.";
                        return ret;
                    }

                    string storagePath = "C:/temp";
                    string fileName = request.NumeroCNH + Path.GetExtension(request.ImagemCNH.FileName);
                    string filePath = Path.Combine(storagePath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.ImagemCNH.CopyToAsync(stream);
                    }

                    string cnhUrl = "C://temp//" + request.NumeroCNH;

                    var newEntregador = new Entregadores
                    {
                        Nome = request.Nome,
                        CNPJ = request.CNPJ,
                        DataNascimento = dataNascimento,
                        NumeroCNH = request.NumeroCNH,
                        TipoCNH = request.TipoCNH,
                        ImagemCNH = cnhUrl
                    };

                    _context.Entregadores.Add(newEntregador);
                    await _context.SaveChangesAsync();

                }
                ret.StatusCode = System.Net.HttpStatusCode.OK;
                ret.IsSucess = true;
                ret.Message = "Entregador cadastrado com sucesso.";
                return ret;
            }
            catch(Exception ex)
            {
                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                ret.Message = ex.Message;
                return ret;
            }
        }

        public async Task<BaseResponse> UpdateEntregador(UpdateEntregadorRequest request)
        {
            try
            {
                var currentData = _context.Entregadores.FirstOrDefault(p => p.NumeroCNH == request.NumeroCNH);

                if (currentData != null)
                {
                    string storagePath = "C:/temp";
                    string fileName = request.NumeroCNH + Path.GetExtension(request.ImagemCNH.FileName);
                    string filePath = Path.Combine(storagePath, fileName);

                    string cnhUrl = "C://temp//" + request.NumeroCNH;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.ImagemCNH.CopyToAsync(stream);
                    }

                    currentData.ImagemCNH = cnhUrl;

                    await _context.SaveChangesAsync();

                    ret.StatusCode = System.Net.HttpStatusCode.OK;
                    ret.IsSucess = true;
                    ret.Message = "Entregador atualizado com sucesso.";
                    return ret;
                }

                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                ret.Message = "Entregador não encontrado.";
                return ret;
            }
            catch(Exception ex)
            {
                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                ret.Message = ex.Message;
                return ret;
            }
        }

        public async Task<BaseResponse> GetEntregadoresNotifications(NotificationEntregadoresRequest request)
        {
            try
            {
                PermissionService permission = new PermissionService(_context);
                List<int> ids = new List<int>();

                bool userAdmin = permission.CheckPermission(request.NomeUsuario);
                if (userAdmin)
                {
                    var entregadoresNotifications = _context.Notificacoes.Where(n => n.IdPedido == request.IdPedido).Select(n => n.Body).ToList();

                    foreach (var ret in entregadoresNotifications)
                    {
                        var obj = JsonConvert.DeserializeObject<Pedidos>(ret);
                        ids.Add(obj.EntregadorId);
                    }

                    ret.StatusCode = System.Net.HttpStatusCode.OK;
                    ret.IsSucess = true;
                    ret.Message = string.Join(",", ids);
                    return ret;
                }
                ret.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                ret.IsSucess = false;
                ret.Message = "Usuário não autorizado.";
                return ret;
            }
            catch(Exception ex)
            {
                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                ret.Message = ex.Message;
                return ret;
            }
        }

        public async Task<BaseResponse> AcceptPedido(AcceptPedidoRequest request)
        {
            try
            {
                var entregador = _context.Entregadores.Where(e => e.NumeroCNH == request.numeroCNH).FirstOrDefault();

                if (entregador == null)
                {
                    ret.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    ret.IsSucess = false;
                    ret.Message = "Entregador não encontrado.";
                    return ret;
                }

                var pedido = _context.Pedidos.Where(p => p.Id == request.IdPedido).FirstOrDefault();

                if (pedido == null)
                {
                    ret.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    ret.IsSucess = false;
                    ret.Message = "Pedido não encontrado.";
                    return ret;
                }

                pedido.EntregadorId = entregador.Id;
                pedido.Situacao = "Aceito";

                await _context.SaveChangesAsync();

                ret.StatusCode = System.Net.HttpStatusCode.OK;
                ret.IsSucess = true;
                ret.Message = "Pedido Aceito com sucesso.";
                return ret;
            }
            catch
            {
                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                return ret;
            }
        }

        public async Task<BaseResponse> DeliverPedido(DeliverPedidoRequest request)
        {
            try
            {
                var entregador = _context.Entregadores.Where(e => e.NumeroCNH == request.numeroCNH).FirstOrDefault();

                if (entregador == null)
                {
                    ret.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    ret.IsSucess = false;
                    ret.Message = "Entregador não encontrado.";
                    return ret;
                }

                var pedido = _context.Pedidos.Where(p => p.Id == request.IdPedido).FirstOrDefault();

                if (pedido == null)
                {
                    ret.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    ret.IsSucess = false;
                    ret.Message = "Pedido não encontrado.";
                    return ret;
                }

                pedido.Situacao = "Entregue";

                await _context.SaveChangesAsync();

                ret.StatusCode = System.Net.HttpStatusCode.OK;
                ret.IsSucess = true;
                ret.Message = "Pedido Entregue com sucesso.";
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
