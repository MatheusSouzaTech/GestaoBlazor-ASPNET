using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class UnidadeMedidaService
    {
        private readonly AppDbContext _context;
        public UnidadeMedidaService(AppDbContext context) => _context = context;

        public async Task<List<UnidadeMedida>> ListarUnidades() =>
            await _context.UnidadesMedida.ToListAsync();

        public async Task<UnidadeMedida?> BuscarUnidadeId(int id) =>
            await _context.UnidadesMedida.FindAsync(id);

        public async Task<(bool sucesso, string mensagem)> Adicionar(UnidadeMedida u)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(u.Nome))
                    return (false, "O nome da unidade de medida é obrigatório.");
                if (string.IsNullOrWhiteSpace(u.Sigla))
                    return (false, "A sigla da unidade de medida é obrigatória.");

                _context.UnidadesMedida.Add(u);
                await _context.SaveChangesAsync();
                return (true, "Unidade de medida cadastrada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar a unidade de medida. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Atualizar(int id, UnidadeMedida u)
        {
            try
            {
                var atual = await _context.UnidadesMedida.FindAsync(id);
                if (atual == null)
                    return (false, "Unidade de medida não encontrada.");

                if (!string.IsNullOrWhiteSpace(u.Nome)) atual.Nome = u.Nome;
                if (!string.IsNullOrWhiteSpace(u.Sigla)) atual.Sigla = u.Sigla;

                await _context.SaveChangesAsync();
                return (true, "Unidade de medida atualizada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar a unidade de medida. Verifique os dados informados.");
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
                var u = await _context.UnidadesMedida.FindAsync(id);
                if (u == null)
                    return (false, "Unidade de medida não encontrada.");

                _context.UnidadesMedida.Remove(u);
                await _context.SaveChangesAsync();
                return (true, "Unidade de medida excluída com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir esta unidade pois ela está vinculada a produtos.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
