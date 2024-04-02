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
    public class PedidoService : IPedidoService
    {
        private readonly AppSettings _settings;
        private readonly ILogger<IPedidoService> _logger;
        private readonly IApplicationContext _context;


        public PedidoService(IOptions<AppSettings> settings, ILogger<IPedidoService> logger, IApplicationContext context)
        {
            _settings = settings.Value;
            _logger = logger;
            _context = context;
        }

        BaseResponse ret = new BaseResponse();

        public async Task<BaseResponse> NewPedido(PedidoRequest request)
        {
            PublisherService publisherService = new PublisherService(_context);
            PermissionService permission = new PermissionService(_context);

            try
            {
                bool userAdmin = permission.CheckPermission(request.NomeUsuario);

                if (userAdmin)
                {
                    var newPedido = new Pedidos
                    {
                        DataCriacao = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")).ToUniversalTime(),
                        ValorCorrida = request.ValorCorrida,
                        Situacao = request.Situacao,
                        NotificacaoRecebida = true,
                        EntregadorId = 0
                    };

                    _context.Pedidos.Add(newPedido);
                    await _context.SaveChangesAsync();

                    int ultimoPedido = _context.Pedidos.Max(e => e.Id);

                    var newNotificacao = new NotificationRequest
                    {
                        IdPedido = ultimoPedido,
                        DataCriacao = DateTime.Now.ToString("yyyy-MM-dd"),
                        ValorCorrida = request.ValorCorrida,
                        Situacao = request.Situacao
                    };

                    var retPublisher = await publisherService.PublisherNotification(newNotificacao);

                    if (retPublisher.IsSucess)
                    {
                        var newMessage = new Notificacoes
                        {
                            IdPedido = ultimoPedido,
                            Body = JsonConvert.SerializeObject(newNotificacao)
                        };
                        _context.Notificacoes.Add(newMessage);
                        await _context.SaveChangesAsync();

                        ret.StatusCode = System.Net.HttpStatusCode.OK;
                        ret.IsSucess = true;
                        ret.Message = "Novo pedido adicionado com sucesso.";
                        return ret;
                    }

                    ret.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    ret.IsSucess = false;
                    ret.Message = "Erro ao publicar mensagem.";
                    return ret;
                }

                ret.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                ret.IsSucess = false;
                ret.Message = "Usuário não autorizado.";
                return ret;
            }
            catch (Exception ex)
            {
                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                ret.Message = ex.Message;
                return ret;
            }
        }


    }
}
