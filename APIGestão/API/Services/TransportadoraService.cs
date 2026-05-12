using APIGestão.API.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestão.API.Services
{
    public class TransportadoraService
    {
        private readonly AppDbContext _context;

        public TransportadoraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transportadora>> ListarTransportadoras()
        {
            return await _context.Transportadoras.ToListAsync();
        }

        public async Task<Transportadora> BuscarTransportadoraId(int id)
        {
            var busca = await _context.Transportadoras.FirstOrDefaultAsync(t => t.ID == id);
            return busca;
        }

        public async Task<bool> AdicionarTransportadora(Transportadora t)
        {
            try
            {
                var transportadora = new Transportadora
                {
                    RazaoSocial = t.RazaoSocial,
                    NomeFantasia = t.NomeFantasia,
                    Telefone = t.Telefone,
                    Email = t.Email,
                    TipoTransporte = t.TipoTransporte,
                    PrazoEntrega = t.PrazoEntrega,
                    Status = t.Status
                };
                _context.Transportadoras.Add(transportadora);
                await _context.SaveChangesAsync();
                Console.WriteLine("Transportadora Cadastrada com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar transportadora" + ex);
                throw;
            }
        }
         public async Task<bool> ExcluirTransportadora(int id)
        {
            try
            {
                var transportadora = await _context.Transportadoras.FindAsync(id);
                if (transportadora == null)
                {
                    Console.WriteLine("Transportadora não encontrada.");
                    return false;
                }
                _context.Transportadoras.Remove(transportadora);
                await _context.SaveChangesAsync();
                Console.WriteLine("Transportadora excluída com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir transportadora" + ex);
                throw;
            }
         }

        public async Task<bool> AtualizarTransportadora(int id, Transportadora t)
        {
            try
            {
                var transportadora = await _context.Transportadoras.FindAsync(id);
                if (transportadora == null)
                {
                    Console.WriteLine("Transportadora não encontrada.");
                    return false;
                }
                transportadora.RazaoSocial = t.RazaoSocial;
                transportadora.NomeFantasia = t.NomeFantasia;
                transportadora.Telefone = t.Telefone;
                transportadora.Email = t.Email;
                transportadora.TipoTransporte = t.TipoTransporte;
                transportadora.PrazoEntrega = t.PrazoEntrega;
                transportadora.Status = t.Status;
                _context.Transportadoras.Update(transportadora);
                await _context.SaveChangesAsync();
                Console.WriteLine("Transportadora atualizada com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar transportadora" + ex);
                throw;
            }
        }
    }
}
