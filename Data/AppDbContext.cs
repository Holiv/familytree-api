using FamilyTree.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<RegistroPessoa> RegistrosPessoa { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedidos { get; set; }
        public DbSet<ValidationToken> ValidationTokens { get; set; }
        public DbSet<Produto> Produtos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pessoa: Pai ↔ FilhosDoPai
            modelBuilder.Entity<Pessoa>()
                .HasMany(p => p.FilhosDoPai)
                .WithOne(p => p.Pai)
                .HasForeignKey(p => p.PaiId)
                .OnDelete(DeleteBehavior.Cascade);

            // Pessoa: Mãe ↔ FilhosDaMae
            modelBuilder.Entity<Pessoa>()
                .HasMany(p => p.FilhosDaMae)
                .WithOne(p => p.Mae)
                .HasForeignKey(p => p.MaeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Pessoa: Conjuge (auto-relacionamento 1:1)
            modelBuilder.Entity<Pessoa>()
                .HasOne(p => p.Conjuge)
                .WithOne()
                .HasForeignKey<Pessoa>(p => p.ConjugeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice único para CPF
            modelBuilder.Entity<Pessoa>()
                .HasIndex(p => p.CPF)
                .IsUnique();

            modelBuilder.Entity<Pessoa>()
                .HasOne(p => p.Usuario)
                .WithOne(u => u.Pessoa)
                .HasForeignKey<Pessoa>(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pessoa>()
                .HasOne(p => p.CriadorUsuario)
                .WithMany()
                .HasForeignKey(p => p.CriadorUsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Usuário → Pessoa
            // modelBuilder.Entity<Usuario>()
            //     .HasOne(u => u.Pessoa)
            //     .WithMany()
            //     .HasForeignKey(u => u.PessoaId)
            //     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ValidationToken → Pessoa
            modelBuilder.Entity<ValidationToken>()
                .HasOne(t => t.Pessoa)
                .WithMany()
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            // ValidationToken → Emitente (Usuário)
            modelBuilder.Entity<ValidationToken>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(t => t.GeradoPorUsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ValidationToken>()
                .HasIndex(t => t.Token)
                .IsUnique();

            // RegistroPessoa: Chave composta
            modelBuilder.Entity<RegistroPessoa>()
                .HasKey(rp => new { rp.RegistroId, rp.PessoaId });

            modelBuilder.Entity<RegistroPessoa>()
                .HasOne(rp => rp.Registro)
                .WithMany(r => r.RegistroPessoas)
                .HasForeignKey(rp => rp.RegistroId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RegistroPessoa>()
                .HasOne(rp => rp.Pessoa)
                .WithMany(r => r.RegistroPessoas)
                .HasForeignKey(rp => rp.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemPedido → Pedido (N:1)
            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Produto: Nome único
            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.Nome)
                .IsUnique();

            // Pedido → Usuario
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemPedido → Produto
            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
