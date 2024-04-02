using System.Text.Json.Serialization;

namespace Mottu.Service.Request
{
    public class NotificationRequest
    {
        public int IdPedido { get; set; }
        public string DataCriacao { get; set; }
        public decimal ValorCorrida { get; set; }
        public string Situacao { get; set; }
        [JsonIgnore]
        public int EntregadorId { get; set; }
    }
}
