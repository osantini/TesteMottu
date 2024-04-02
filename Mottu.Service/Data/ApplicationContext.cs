using Mottu.Service.Models;
using Microsoft.EntityFrameworkCore;
using Mottu.Service.Interfaces;
using MediatR;


namespace Mottu.Service.Data
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Motos> Motos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Entregadores> Entregadores { get; set; }
        public DbSet<AluguelMotos> AluguelMotos { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Notificacoes> Notificacoes { get; set; }

        private readonly IMediator _mediator;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}