using Microsoft.EntityFrameworkCore;
using VimaV2.Models;

namespace VimaV2.Database
{
    public partial class VimaV2DbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }

        public VimaV2DbContext()
        {
        }

        public VimaV2DbContext(DbContextOptions<VimaV2DbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("8.0.37-mysql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            // Configurações do campo `Tamanhos`
            modelBuilder.Entity<Produto>()
                .Property(p => p.Tamanhos)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            // Configuração de `ImageURL` sem conversão (string simples)
            modelBuilder.Entity<Produto>()
                .Property(p => p.ImageURL);

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
