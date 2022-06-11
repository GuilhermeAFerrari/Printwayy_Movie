global using Microsoft.EntityFrameworkCore;
using PrintWayyMovie_API;

namespace PrintWayyMovie_API
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Filme> Filme => Set<Filme>();
        public DbSet<Sala> Sala => Set<Sala>();
        public DbSet<Sessao> Sessao => Set<Sessao>();
    }
}