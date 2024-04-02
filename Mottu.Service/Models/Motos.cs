
namespace Mottu.Service.Models
{
    public class Motos
    {
        public int Id { get; set; }
        public string Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Status { get; set; }
        public int EntregadorId { get; set; }
    }
}
