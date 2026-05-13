using ModelsLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestão.API.Services
{
    public class FornecedorService
    {
        private readonly AppDbContext _context;
        public FornecedorService(AppDbContext context) => _context = context;

        public async Task<List<Fornecedor>> ListarFornecedores() =>
            await _context.Fornecedores.ToListAsync();

        public async Task<Fornecedor?> BuscarFornecedorId(int id) =>
            await _context.Fornecedores.Include(f => f.Enderecos).FirstOrDefaultAsync(f => f.Id == id);

        public async Task<(bool sucesso, string mensagem)> AdicionarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fornecedor.RazaoSocial))
                    return (false, "A Razão Social do fornecedor é obrigatória.");

                _context.Fornecedores.Add(fornecedor);
                await _context.SaveChangesAsync();
                return (true, "Fornecedor cadastrado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar o fornecedor. Verifique se o CNPJ já está cadastrado.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarFornecedor(int id, Fornecedor fornecedor)
        {
            try
            {
                var atual = await _context.Fornecedores.FindAsync(id);
                if (atual == null)
                    return (false, "Fornecedor não encontrado.");

                if (!string.IsNullOrWhiteSpace(fornecedor.RazaoSocial)) atual.RazaoSocial = fornecedor.RazaoSocial;
                if (!string.IsNullOrWhiteSpace(fornecedor.CNPJ)) atual.CNPJ = fornecedor.CNPJ;
                if (!string.IsNullOrWhiteSpace(fornecedor.Email)) atual.Email = fornecedor.Email;
                if (!string.IsNullOrWhiteSpace(fornecedor.Telefone)) atual.Telefone = fornecedor.Telefone;
                atual.Ativo = fornecedor.Ativo;

                await _context.SaveChangesAsync();
                return (true, "Fornecedor atualizado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar o fornecedor. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirFornecedor(int id)
        {
            try
            {
                var fornecedor = await _context.Fornecedores.FindAsync(id);
                if (fornecedor == null)
                    return (false, "Fornecedor não encontrado.");

                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
                return (true, "Fornecedor excluído com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir este fornecedor pois ele possui registros vinculados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
