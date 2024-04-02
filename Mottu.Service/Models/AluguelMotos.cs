
namespace Mottu.Service.Models
{
    public class AluguelMotos
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public int EntregadorId { get; set; }
        public string Status { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal ValorFinal { get; set; }
    }
}
