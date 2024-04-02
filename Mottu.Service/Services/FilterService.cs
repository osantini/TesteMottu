using Mottu.Service.Models;
using Mottu.Service.Request;

namespace Mottu.Service.Services
{
    public static class FilterService
    {
        public static IQueryable<Motos> FilterMotos(this IQueryable<Motos> consulta, GetMotoRequest request)
        {
            consulta = consulta.Where(p =>
            (string.IsNullOrEmpty(request.Placa) || p.Placa == request.Placa));

            return consulta;
        }
    }
}
