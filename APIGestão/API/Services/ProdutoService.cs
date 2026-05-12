using APIGestão.API.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestão.API.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produtos>> ListarProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produtos> BuscarProdutoId(int id)
        {
            var busca = await _context.Produtos.FirstOrDefaultAsync(p => p.ID == id);
            return busca;
        }

        public async Task<bool> AdicionarProdutos(Produtos p)
        {
            try {

                var produto = new Produtos
                {
                    Nome = p.Nome,
                    IDMarca = p.IDMarca,
                    Quantidade = p.Quantidade,
                    TipoMercadoria = p.TipoMercadoria,
                    Status = p.Status,
                    DataCadastro = p.DataCadastro
                };

                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();

                Console.WriteLine("Produto Cadastrado com sucesso!");

                return true;

            }catch(Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar produto" + ex);
                throw;
            }
        }

        public async Task<bool> ExcluirProdutos(int id)
        {
            try {

                var buscar = await _context.Produtos.FirstOrDefaultAsync(p => p.ID == id);

                if(buscar == null)
                {
                    Console.WriteLine("Produto não encontrado");
                    return false;
                }

                _context.Produtos.Remove(buscar);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao remover produto " + ex);
                throw;
            }
        }

        public async Task<bool> UpdateProdutos(int id, Produtos p)
        {
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.ID == id); 
                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado");
                    return false;
                }

                produto.Nome = p.Nome;
                produto.IDMarca = p.IDMarca;
                produto.Quantidade = p.Quantidade;
                produto.TipoMercadoria = p.TipoMercadoria;
                produto.Status = p.Status;
                produto.DataCadastro = p.DataCadastro;

                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();

                Console.WriteLine("Produto atualizado com sucesso!");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar produto: " + ex.Message);
                throw;
            }
        }


    }
}
