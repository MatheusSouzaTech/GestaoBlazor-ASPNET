using APIGestão.API.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestão.API.Services
{
    public class FornecedorService
    {
        private readonly AppDbContext _context;

        public FornecedorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Fornecedores>> ListarFornecedores()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        public async Task<Fornecedores> BuscarFornecedorId(int id)
        {
            var busca = await _context.Fornecedores.FirstOrDefaultAsync(f => f.ID == id);
            return busca;
        }

        public async Task<bool> AdicionarFornecedor(Fornecedores f)
        {
            try
            {
                var fornecedor = new Fornecedores
                {
                    Nome = f.Nome,
                    CNPJ = f.CNPJ,
                    Endereco = f.Endereco,
                    Telefone = f.Telefone,
                    Status = f.Status,
                    DataCadastro = f.DataCadastro
                };
                _context.Fornecedores.Add(fornecedor);
                await _context.SaveChangesAsync();
                Console.WriteLine("Fornecedor Cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar fornecedor" + ex);
                throw;
            }
        }
        public async Task<bool> ExcluirFornecedor(int id)
        {
            try
            {
                var fornecedor = await _context.Fornecedores.FindAsync(id);
                if (fornecedor == null)
                {
                    Console.WriteLine("Fornecedor não encontrado.");
                    return false;
                }
                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
                Console.WriteLine("Fornecedor excluído com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir fornecedor: " + ex);
                throw;
            }
        }
        public async Task<bool> AtualizarFornecedor(int id, Fornecedores f)
        {
            try
            {
                var fornecedor = await _context.Fornecedores.FindAsync(id);
                if (fornecedor == null)
                {
                    Console.WriteLine("Fornecedor não encontrado.");
                    return false;
                }
                fornecedor.Nome = f.Nome;
                fornecedor.CNPJ = f.CNPJ;
                fornecedor.Endereco = f.Endereco;
                fornecedor.Telefone = f.Telefone;
                fornecedor.Status = f.Status;
                fornecedor.DataCadastro = f.DataCadastro;
                _context.Fornecedores.Update(fornecedor);
                await _context.SaveChangesAsync();
                Console.WriteLine("Fornecedor atualizado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar fornecedor: " + ex);
                throw;
            }
        }
    }
}
