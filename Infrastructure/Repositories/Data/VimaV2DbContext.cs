using Microsoft.EntityFrameworkCore;
using VimaV2.Models;

namespace VimaV2.Repositories.Data
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
