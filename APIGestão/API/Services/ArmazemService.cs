using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class ArmazemService
    {
        private readonly AppDbContext _context;
        public ArmazemService(AppDbContext context) => _context = context;

        public async Task<List<Armazem>> ListarArmazens() =>
            await _context.Armazens.Include(a => a.Filial).ToListAsync();

        public async Task<Armazem?> BuscarArmazemId(int id) =>
            await _context.Armazens.Include(a => a.Filial).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<(bool sucesso, string mensagem)> Adicionar(Armazem a)
        {
            try
            {
                if (a.FilialId <= 0)
                    return (false, "A filial é obrigatória para o armazém.");

                var filialExiste = await _context.Filiais.AnyAsync(f => f.Id == a.FilialId);
                if (!filialExiste)
                    return (false, "A filial informada não existe.");

                _context.Armazens.Add(a);
                await _context.SaveChangesAsync();
                return (true, "Armazém cadastrado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar o armazém. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Atualizar(int id, Armazem a)
        {
            try
            {
                var atual = await _context.Armazens.FindAsync(id);
                if (atual == null)
                    return (false, "Armazém não encontrado.");

                if (!string.IsNullOrWhiteSpace(a.Nome)) atual.Nome = a.Nome;
                if (a.FilialId > 0) atual.FilialId = a.FilialId;

                await _context.SaveChangesAsync();
                return (true, "Armazém atualizado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar o armazém. Verifique os dados informados.");
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
                var a = await _context.Armazens.FindAsync(id);
                if (a == null)
                    return (false, "Armazém não encontrado.");

                _context.Armazens.Remove(a);
                await _context.SaveChangesAsync();
                return (true, "Armazém excluído com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir este armazém pois ele possui endereçamentos vinculados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
