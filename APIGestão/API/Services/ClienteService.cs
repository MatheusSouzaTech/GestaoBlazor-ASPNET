using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;
        public ClienteService(AppDbContext context) => _context = context;

        public async Task<List<Cliente>> ListarClientes() =>
            await _context.Clientes.ToListAsync();

        public async Task<Cliente?> BuscarClienteId(int id) =>
            await _context.Clientes.Include(c => c.Enderecos).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<(bool sucesso, string mensagem)> AdicionarCliente(Cliente cliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cliente.Nome))
                    return (false, "O nome do cliente é obrigatório.");

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return (true, "Cliente cadastrado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar o cliente. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarCliente(int id, Cliente cliente)
        {
            try
            {
                var atual = await _context.Clientes.FindAsync(id);
                if (atual == null)
                    return (false, "Cliente não encontrado.");

                if (!string.IsNullOrWhiteSpace(cliente.Nome)) atual.Nome = cliente.Nome;
                if (!string.IsNullOrWhiteSpace(cliente.CPF_CNPJ)) atual.CPF_CNPJ = cliente.CPF_CNPJ;
                if (!string.IsNullOrWhiteSpace(cliente.Email)) atual.Email = cliente.Email;
                if (!string.IsNullOrWhiteSpace(cliente.Telefone)) atual.Telefone = cliente.Telefone;
                if (cliente.TipoPessoa > 0) atual.TipoPessoa = cliente.TipoPessoa;
                atual.Ativo = cliente.Ativo;

                await _context.SaveChangesAsync();
                return (true, "Cliente atualizado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar o cliente. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirCliente(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                    return (false, "Cliente não encontrado.");

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                return (true, "Cliente excluído com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir este cliente pois ele possui registros vinculados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
