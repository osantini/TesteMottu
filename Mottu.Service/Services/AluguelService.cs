using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mottu.Infra;
using Mottu.Service.Interfaces;
using Mottu.Service.Models;
using Mottu.Service.Request;
using Mottu.Service.Responses;
using System.Globalization;

namespace Mottu.Service.Services
{
    public class AluguelService : IAluguelService
    {
        private readonly AppSettings _settings;
        private readonly ILogger<IAluguelService> _logger;
        private readonly IApplicationContext _context;
        BaseResponse ret = new BaseResponse();

        public AluguelService(IOptions<AppSettings> settings, ILogger<IAluguelService> logger, IApplicationContext context)
        {
            _settings = settings.Value;
            _logger = logger;
            _context = context;
        }

        public async Task<BaseResponse> GetValor(GetValorRequest request)
        {
            try
            {
                var dataInicio = Convert.ToDateTime(request.DataInicio).AddDays(1).ToUniversalTime();
                var dataFim = Convert.ToDateTime(request.DataFim).ToUniversalTime();
                var dataPrevisaoFim = Convert.ToDateTime(request.DataPrevisaoFim).ToUniversalTime();

                var valorFinal = CalculateFinalValue(dataInicio, dataFim, dataPrevisaoFim);

                ret.StatusCode = System.Net.HttpStatusCode.OK;
                ret.IsSucess = true;
                ret.Message = "O valor do aluguel é R$ " + valorFinal.ToString();
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

        public async Task<BaseResponse> RentMoto(RentMotoRequest request)
        {
            try
            {
                var cnh = _context.Entregadores.FirstOrDefault(e => e.NumeroCNH == request.NumeroCNH);
                var moto = _context.Motos.FirstOrDefault(m => m.Status == "Disponivel");

                if (cnh == null)
                {
                    ret.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    ret.IsSucess = false;
                    ret.Message = "Entregador não cadastrado.";
                    return ret;
                }

                if (cnh.TipoCNH != "A")
                {
                    ret.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    ret.IsSucess = false;
                    ret.Message = "CNH divergente do tipo A.";
                    return ret;
                }

                if (moto == null)
                {
                    ret.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    ret.IsSucess = false;
                    ret.Message = "Nenhuma moto disponivel no momento.";
                    return ret;
                }

                var dataInicio = Convert.ToDateTime(request.DataInicio).AddDays(1).ToUniversalTime();
                var dataFim = Convert.ToDateTime(request.DataFim).ToUniversalTime();
                var dataPrevisaoFim = Convert.ToDateTime(request.DataPrevisaoFim).ToUniversalTime();

                var newRent = new AluguelMotos
                {
                    MotoId = moto.Id,
                    EntregadorId = cnh.Id,
                    Status = "Ativo",
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    ValorFinal = CalculateFinalValue(dataInicio, dataFim, dataPrevisaoFim)
                };

                _context.AluguelMotos.Add(newRent);
                await _context.SaveChangesAsync();

                ret.StatusCode = System.Net.HttpStatusCode.OK;
                ret.IsSucess = true;
                ret.Message = "Moto alugada com sucesso.";
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

        public decimal CalculateFinalValue(DateTime dataInicio, DateTime dataFim, DateTime dataPrevisaoFim)
        {
            TimeSpan duracaoAluguel = dataFim - dataInicio;
            int diasAluguel = duracaoAluguel.Days;

            decimal valorTotal = 0;

            //Verifica o plano de aluguel baseado na duração
            if (diasAluguel >= 30)
            {
                valorTotal = diasAluguel * 22.00m;
            }
            else if (diasAluguel >= 15)
            {
                valorTotal = diasAluguel * 28.00m;
            }
            else if (diasAluguel >= 7)
            {
                valorTotal = diasAluguel * 30.00m;
            }
            else
            {
                throw new ArgumentException("A duração mínima do aluguel é de 7 dias.");
            }

            //Verifica se a data informada é superior à data prevista do término
            if (dataPrevisaoFim > dataFim)
            {
                int diasAdicionais = (dataPrevisaoFim - dataFim).Days;

                decimal valorAdicional = diasAdicionais * 50.00m;
                valorTotal += valorAdicional;
            }

            //Calcula a multa adicional se a data informada for inferior à data prevista do término
            if (dataPrevisaoFim < dataFim)
            {
                int diasNaoEfetivados = (dataFim - dataPrevisaoFim).Days;

                decimal multa = 0;
                if (diasAluguel >= 30)
                {
                    multa = diasNaoEfetivados * 22.00m * 0.60m; // 60% sobre o valor das diárias não efetivadas
                }
                else if (diasAluguel >= 15)
                {
                    multa = diasNaoEfetivados * 28.00m * 0.40m; // 40% sobre o valor das diárias não efetivadas
                }
                else if (diasAluguel >= 7)
                {
                    multa = diasNaoEfetivados * 30.00m * 0.20m; // 20% sobre o valor das diárias não efetivadas
                }

                valorTotal += multa;
            }

            return valorTotal;
        }
    }
}

