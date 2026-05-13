using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class EmpresaService
    {
        private readonly AppDbContext _context;
        public EmpresaService(AppDbContext context) => _context = context;

        public async Task<List<Empresa>> ListarEmpresas() =>
            await _context.Empresas.Include(e => e.Filiais).ToListAsync();

        public async Task<Empresa?> BuscarEmpresaId(int id) =>
            await _context.Empresas.Include(e => e.Filiais).FirstOrDefaultAsync(e => e.Id == id);

        public async Task<(bool sucesso, string mensagem)> AdicionarEmpresa(Empresa empresa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(empresa.RazaoSocial))
                    return (false, "A Razão Social é obrigatória.");
                if (string.IsNullOrWhiteSpace(empresa.CNPJ))
                    return (false, "O CNPJ é obrigatório.");

                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();
                return (true, "Empresa cadastrada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar a empresa. Verifique se o CNPJ já está cadastrado.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarEmpresa(int id, Empresa empresa)
        {
            try
            {
                var atual = await _context.Empresas.FindAsync(id);
                if (atual == null)
                    return (false, "Empresa não encontrada.");

                if (!string.IsNullOrWhiteSpace(empresa.RazaoSocial)) atual.RazaoSocial = empresa.RazaoSocial;
                if (!string.IsNullOrWhiteSpace(empresa.NomeFantasia)) atual.NomeFantasia = empresa.NomeFantasia;
                if (!string.IsNullOrWhiteSpace(empresa.CNPJ)) atual.CNPJ = empresa.CNPJ;
                if (empresa.RegimeTributario > 0) atual.RegimeTributario = empresa.RegimeTributario;
                atual.Ativo = empresa.Ativo;

                await _context.SaveChangesAsync();
                return (true, "Empresa atualizada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar a empresa. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirEmpresa(int id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa == null)
                    return (false, "Empresa não encontrada.");

                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
                return (true, "Empresa excluída com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir esta empresa pois ela possui filiais vinculadas.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
