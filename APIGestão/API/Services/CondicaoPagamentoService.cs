using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class CondicaoPagamentoService
    {
        private readonly AppDbContext _context;
        public CondicaoPagamentoService(AppDbContext context) => _context = context;

        public async Task<List<CondicaoPagamento>> Listar() =>
            await _context.CondicoesPagamento.Include(c => c.Parcelas).ToListAsync();

        public async Task<CondicaoPagamento?> BuscarId(int id) =>
            await _context.CondicoesPagamento.Include(c => c.Parcelas).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<(bool sucesso, string mensagem)> Adicionar(CondicaoPagamento c)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(c.Nome))
                    return (false, "O nome da condição de pagamento é obrigatório.");

                _context.CondicoesPagamento.Add(c);
                await _context.SaveChangesAsync();
                return (true, "Condição de pagamento cadastrada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar a condição de pagamento. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Atualizar(int id, CondicaoPagamento c)
        {
            try
            {
                var atual = await _context.CondicoesPagamento.FindAsync(id);
                if (atual == null)
                    return (false, "Condição de pagamento não encontrada.");

                if (!string.IsNullOrWhiteSpace(c.Nome)) atual.Nome = c.Nome;

                await _context.SaveChangesAsync();
                return (true, "Condição de pagamento atualizada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar a condição de pagamento. Verifique os dados informados.");
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
                var c = await _context.CondicoesPagamento.FindAsync(id);
                if (c == null)
                    return (false, "Condição de pagamento não encontrada.");

                _context.CondicoesPagamento.Remove(c);
                await _context.SaveChangesAsync();
                return (true, "Condição de pagamento excluída com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir esta condição pois ela possui parcelas vinculadas.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
