namespace Mottu.Service.Models
{
    public class Pedidos 
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorCorrida { get; set; }
        public string Situacao { get; set; }
        public bool NotificacaoRecebida { get; set; }
        public int EntregadorId { get; set; }
    }
}
