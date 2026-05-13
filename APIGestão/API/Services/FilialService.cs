using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class FilialService
    {
        private readonly AppDbContext _context;
        public FilialService(AppDbContext context) => _context = context;

        public async Task<List<Filial>> ListarFiliais() =>
            await _context.Filiais.Include(f => f.Empresa).ToListAsync();

        public async Task<Filial?> BuscarFilialId(int id) =>
            await _context.Filiais.Include(f => f.Empresa).FirstOrDefaultAsync(f => f.Id == id);

        public async Task<(bool sucesso, string mensagem)> AdicionarFilial(Filial filial)
        {
            try
            {
                if (filial.EmpresaId <= 0)
                    return (false, "A empresa vinculada é obrigatória.");

                var empresaExiste = await _context.Empresas.AnyAsync(e => e.Id == filial.EmpresaId);
                if (!empresaExiste)
                    return (false, "A empresa informada não existe.");

                _context.Filiais.Add(filial);
                await _context.SaveChangesAsync();
                return (true, "Filial cadastrada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar a filial. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarFilial(int id, Filial filial)
        {
            try
            {
                var atual = await _context.Filiais.FindAsync(id);
                if (atual == null)
                    return (false, "Filial não encontrada.");

                if (!string.IsNullOrWhiteSpace(filial.Nome)) atual.Nome = filial.Nome;
                if (!string.IsNullOrWhiteSpace(filial.CNPJ)) atual.CNPJ = filial.CNPJ;
                if (filial.EmpresaId > 0) atual.EmpresaId = filial.EmpresaId;
                atual.Ativo = filial.Ativo;

                await _context.SaveChangesAsync();
                return (true, "Filial atualizada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar a filial. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirFilial(int id)
        {
            try
            {
                var filial = await _context.Filiais.FindAsync(id);
                if (filial == null)
                    return (false, "Filial não encontrada.");

                _context.Filiais.Remove(filial);
                await _context.SaveChangesAsync();
                return (true, "Filial excluída com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir esta filial pois ela possui registros vinculados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
