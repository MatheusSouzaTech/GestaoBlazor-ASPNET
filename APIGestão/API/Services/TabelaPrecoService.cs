using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class TabelaPrecoService
    {
        private readonly AppDbContext _context;
        public TabelaPrecoService(AppDbContext context) => _context = context;

        public async Task<List<TabelaPreco>> ListarTabelas() =>
            await _context.TabelasPreco.Include(t => t.Itens).ToListAsync();

        public async Task<TabelaPreco?> BuscarTabelaId(int id) =>
            await _context.TabelasPreco.Include(t => t.Itens).FirstOrDefaultAsync(t => t.Id == id);

        public async Task<(bool sucesso, string mensagem)> Adicionar(TabelaPreco t)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(t.Nome))
                    return (false, "O nome da tabela de preço é obrigatório.");

                _context.TabelasPreco.Add(t);
                await _context.SaveChangesAsync();
                return (true, "Tabela de preço cadastrada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar a tabela de preço. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Atualizar(int id, TabelaPreco t)
        {
            try
            {
                var atual = await _context.TabelasPreco.FindAsync(id);
                if (atual == null)
                    return (false, "Tabela de preço não encontrada.");

                if (!string.IsNullOrWhiteSpace(t.Nome)) atual.Nome = t.Nome;

                await _context.SaveChangesAsync();
                return (true, "Tabela de preço atualizada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar a tabela de preço. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Excluir(int id)
        {
            try
            {
                var t = await _context.TabelasPreco.FindAsync(id);
                if (t == null)
                    return (false, "Tabela de preço não encontrada.");

                _context.TabelasPreco.Remove(t);
                await _context.SaveChangesAsync();
                return (true, "Tabela de preço excluída com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir esta tabela pois ela possui itens vinculados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
