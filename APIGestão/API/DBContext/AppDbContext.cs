using ModelsLibrary.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Cadastros base
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Filial> Filiais { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Transportadora> Transportadoras { get; set; }

    // Produtos e catálogo
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Marcas> Marcas { get; set; }
    public DbSet<UnidadeMedida> UnidadesMedida { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    // Estoque
    public DbSet<Armazem> Armazens { get; set; }
    public DbSet<Enderecamento> Enderecamentos { get; set; }

    // Preços e pagamentos
    public DbSet<TabelaPreco> TabelasPreco { get; set; }
    public DbSet<TabelaPrecoItem> TabelaPrecoItens { get; set; }
    public DbSet<CondicaoPagamento> CondicoesPagamento { get; set; }
    public DbSet<CondicaoParcela> CondicaoParcelas { get; set; }

    // Usuários
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Empresa -> Filiais
        modelBuilder.Entity<Filial>()
            .HasOne(f => f.Empresa)
            .WithMany(e => e.Filiais)
            .HasForeignKey(f => f.EmpresaId);

        // Enderecos
        modelBuilder.Entity<Endereco>()
            .HasOne(e => e.Cliente)
            .WithMany(c => c.Enderecos)
            .HasForeignKey(e => e.ClienteId);

        modelBuilder.Entity<Endereco>()
            .HasOne(e => e.Fornecedor)
            .WithMany(f => f.Enderecos)
            .HasForeignKey(e => e.FornecedorId);

        modelBuilder.Entity<Endereco>()
            .HasOne(e => e.Filial)
            .WithMany(f => f.Enderecos)
            .HasForeignKey(e => e.FilialId);

        // Categorias (auto-referência)
        modelBuilder.Entity<Categoria>()
            .HasOne(c => c.CategoriaPai)
            .WithMany(c => c.SubCategorias)
            .HasForeignKey(c => c.CategoriaPaiId);

        // Produto -> Categoria, Marca, UnidadeMedida
        modelBuilder.Entity<Produto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .HasForeignKey(p => p.CategoriaId);

        modelBuilder.Entity<Produto>()
            .HasOne(p => p.Marca)
            .WithMany(m => m.Produtos)
            .HasForeignKey(p => p.MarcaId);

        modelBuilder.Entity<Produto>()
            .HasOne(p => p.UnidadeMedida)
            .WithMany(u => u.Produtos)
            .HasForeignKey(p => p.UnidadeMedidaId);

        // Armazem -> Filial
        modelBuilder.Entity<Armazem>()
            .HasOne(a => a.Filial)
            .WithMany()
            .HasForeignKey(a => a.FilialId);

        // Enderecamento -> Armazem
        modelBuilder.Entity<Enderecamento>()
            .HasOne(e => e.Armazem)
            .WithMany(a => a.Enderecamentos)
            .HasForeignKey(e => e.ArmazemId);

        // TabelaPrecoItem -> TabelaPreco, Produto
        modelBuilder.Entity<TabelaPrecoItem>()
            .HasOne(i => i.TabelaPreco)
            .WithMany(t => t.Itens)
            .HasForeignKey(i => i.TabelaPrecoId);

        modelBuilder.Entity<TabelaPrecoItem>()
            .HasOne(i => i.Produto)
            .WithMany(p => p.TabelaPrecoItens)
            .HasForeignKey(i => i.ProdutoId);

        // CondicaoParcela -> CondicaoPagamento
        modelBuilder.Entity<CondicaoParcela>()
            .HasOne(p => p.CondicaoPagamento)
            .WithMany(c => c.Parcelas)
            .HasForeignKey(p => p.CondicaoPagamentoId);

        // Usuario (auto-referência)
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.UsuarioLider)
            .WithMany()
            .HasForeignKey(u => u.IDUsuarioLider);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Login)
            .IsUnique();
    }
}


