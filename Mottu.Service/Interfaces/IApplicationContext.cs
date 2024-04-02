using Microsoft.EntityFrameworkCore;
using Mottu.Service.Models;

namespace Mottu.Service.Interfaces
{
    public interface IApplicationContext
    {
        public DbSet<Motos> Motos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Entregadores> Entregadores { get; set; }
        public DbSet<AluguelMotos> AluguelMotos { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Notificacoes> Notificacoes { get; set; }

        Task<int> SaveChangesAsync();
    }
}
