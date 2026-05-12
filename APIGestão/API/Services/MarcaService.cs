using APIGestão.API.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestão.API.Services
{
    public class MarcaService
    {
        private readonly AppDbContext _context;

        public MarcaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Marcas>> ListarMarcas()
        {
            return await _context.Marcas.ToListAsync();
        }

        public async Task<Marcas?> BuscarMarcaId(int id)
        {
            return await _context.Marcas.FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<bool> AdicionarMarca(Marcas m)
        {
            try
            {
                var marca = new Marcas
                {
                    Nome = m.Nome
                };
                _context.Marcas.Add(marca);
                await _context.SaveChangesAsync();
                Console.WriteLine("Marca cadastrada com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar marca: " + ex);
                throw;
            }
        }

        public async Task<bool> ExcluirMarca(int id)
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id);
                bool produtosVinculados = await _context.Produtos.AnyAsync(p => p.IDMarca == marca.ID);

                if(produtosVinculados == true)
                {
                    Console.WriteLine("Esta marca possui produtos vinculados");
                    return false;
                }

                if (marca == null)
                {
                    Console.WriteLine("Marca não encontrada.");
                    return false;
                }
                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
                Console.WriteLine("Marca excluída com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir marca: " + ex);
                throw;
            }
        }

        public async Task<bool> AtualizarMarca(int id, Marcas m)
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id);
                if (marca == null)
                {
                    Console.WriteLine("Marca não encontrada.");
                    return false;
                }
                marca.Nome = m.Nome;
                _context.Marcas.Update(marca);
                await _context.SaveChangesAsync();
                Console.WriteLine("Marca atualizada com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar marca: " + ex);
                throw;
            }
        }
    }
}
