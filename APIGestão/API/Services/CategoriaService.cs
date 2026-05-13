using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class CategoriaService
    {
        private readonly AppDbContext _context;
        public CategoriaService(AppDbContext context) => _context = context;

        public async Task<List<Categoria>> ListarCategorias() =>
            await _context.Categorias.Include(c => c.CategoriaPai).ToListAsync();

        public async Task<Categoria?> BuscarCategoriaId(int id) =>
            await _context.Categorias.Include(c => c.SubCategorias).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<(bool sucesso, string mensagem)> AdicionarCategoria(Categoria c)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(c.Nome))
                    return (false, "O nome da categoria é obrigatório.");

                _context.Categorias.Add(c);
                await _context.SaveChangesAsync();
                return (true, "Categoria cadastrada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar a categoria. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarCategoria(int id, Categoria c)
        {
            try
            {
                var atual = await _context.Categorias.FindAsync(id);
                if (atual == null)
                    return (false, "Categoria não encontrada.");

                if (!string.IsNullOrWhiteSpace(c.Nome)) atual.Nome = c.Nome;
                if (c.CategoriaPaiId.HasValue) atual.CategoriaPaiId = c.CategoriaPaiId;

                await _context.SaveChangesAsync();
                return (true, "Categoria atualizada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar a categoria. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirCategoria(int id)
        {
            try
            {
                var cat = await _context.Categorias.FindAsync(id);
                if (cat == null)
                    return (false, "Categoria não encontrada.");

                _context.Categorias.Remove(cat);
                await _context.SaveChangesAsync();
                return (true, "Categoria excluída com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir esta categoria pois ela está vinculada a produtos ou subcategorias.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
