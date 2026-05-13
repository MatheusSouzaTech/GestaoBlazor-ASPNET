using ModelsLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestão.API.Services
{
    public class TransportadoraService
    {
        private readonly AppDbContext _context;
        public TransportadoraService(AppDbContext context) => _context = context;

        public async Task<List<Transportadora>> ListarTransportadoras() =>
            await _context.Transportadoras.ToListAsync();

        public async Task<Transportadora?> BuscarTransportadoraId(int id) =>
            await _context.Transportadoras.FindAsync(id);

        public async Task<(bool sucesso, string mensagem)> AdicionarTransportadora(Transportadora t)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(t.Nome))
                    return (false, "O nome da transportadora é obrigatório.");

                _context.Transportadoras.Add(t);
                await _context.SaveChangesAsync();
                return (true, "Transportadora cadastrada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar a transportadora. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarTransportadora(int id, Transportadora t)
        {
            try
            {
                var atual = await _context.Transportadoras.FindAsync(id);
                if (atual == null)
                    return (false, "Transportadora não encontrada.");

                if (!string.IsNullOrWhiteSpace(t.Nome)) atual.Nome = t.Nome;
                if (!string.IsNullOrWhiteSpace(t.CNPJ)) atual.CNPJ = t.CNPJ;
                if (!string.IsNullOrWhiteSpace(t.Telefone)) atual.Telefone = t.Telefone;
                if (t.Tipo > 0) atual.Tipo = t.Tipo;
                atual.Ativo = t.Ativo;

                await _context.SaveChangesAsync();
                return (true, "Transportadora atualizada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar a transportadora. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirTransportadora(int id)
        {
            try
            {
                var t = await _context.Transportadoras.FindAsync(id);
                if (t == null)
                    return (false, "Transportadora não encontrada.");

                _context.Transportadoras.Remove(t);
                await _context.SaveChangesAsync();
                return (true, "Transportadora excluída com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir esta transportadora pois ela possui registros vinculados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
