using APIGestão.API.Models;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public Microsoft.EntityFrameworkCore.DbSet<Produtos> Produtos { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Marcas> Marcas { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Fornecedores> Fornecedores { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Transportadora> Transportadoras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Produtos>()
             .HasMany(p => p.Fornecedores)
             .WithMany(f => f.Produtos);

        modelBuilder.Entity<Fornecedores>()
            .HasMany(f => f.Marcas)
            .WithMany(m => m.Fornecedores);

        modelBuilder.Entity<Transportadora>()
            .HasMany(t => t.Fornecedores)
            .WithMany(f => f.Transportadoras);

    }
}

