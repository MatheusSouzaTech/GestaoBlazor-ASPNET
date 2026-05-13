using ModelsLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestão.API.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context) => _context = context;

        public async Task<List<Produto>> ListarProdutos() =>
            await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(p => p.UnidadeMedida)
                .ToListAsync();

        public async Task<Produto?> BuscarProdutoId(int id) =>
            await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(p => p.UnidadeMedida)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<(bool sucesso, string mensagem)> AdicionarProdutos(Produto p)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(p.Nome))
                    return (false, "O nome do produto é obrigatório.");

                _context.Produtos.Add(p);
                await _context.SaveChangesAsync();
                return (true, "Produto cadastrado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar o produto. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> UpdateProdutos(int id, Produto p)
        {
            try
            {
                var atual = await _context.Produtos.FindAsync(id);
                if (atual == null)
                    return (false, "Produto não encontrado.");

                if (!string.IsNullOrWhiteSpace(p.Nome)) atual.Nome = p.Nome;
                if (!string.IsNullOrWhiteSpace(p.SKU)) atual.SKU = p.SKU;
                if (!string.IsNullOrWhiteSpace(p.CodigoBarras)) atual.CodigoBarras = p.CodigoBarras;
                if (p.CategoriaId.HasValue) atual.CategoriaId = p.CategoriaId;
                if (p.MarcaId.HasValue) atual.MarcaId = p.MarcaId;
                if (p.UnidadeMedidaId.HasValue) atual.UnidadeMedidaId = p.UnidadeMedidaId;
                if (p.PrecoVenda > 0) atual.PrecoVenda = p.PrecoVenda;
                if (p.Custo > 0) atual.Custo = p.Custo;
                atual.ControlaEstoque = p.ControlaEstoque;
                atual.Ativo = p.Ativo;

                await _context.SaveChangesAsync();
                return (true, "Produto atualizado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar o produto. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirProdutos(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);
                if (produto == null)
                    return (false, "Produto não encontrado.");

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return (true, "Produto excluído com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir este produto pois ele está vinculado a outros registros.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
