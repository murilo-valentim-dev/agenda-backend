using Microsoft.EntityFrameworkCore;
using AluguelApi.Models;

namespace AluguelApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Aluguel> Alugueis { get; set; }
    }
}
