using ModelsLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestão.API.Services
{
    public class MarcaService
    {
        private readonly AppDbContext _context;
        public MarcaService(AppDbContext context) => _context = context;

        public async Task<List<Marcas>> ListarMarcas() =>
            await _context.Marcas.ToListAsync();

        public async Task<Marcas?> BuscarMarcaId(int id) =>
            await _context.Marcas.FindAsync(id);

        public async Task<(bool sucesso, string mensagem)> AdicionarMarca(Marcas m)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(m.Nome))
                    return (false, "O nome da marca é obrigatório.");

                _context.Marcas.Add(m);
                await _context.SaveChangesAsync();
                return (true, "Marca cadastrada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar a marca. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarMarca(int id, Marcas m)
        {
            try
            {
                var atual = await _context.Marcas.FindAsync(id);
                if (atual == null)
                    return (false, "Marca não encontrada.");

                if (!string.IsNullOrWhiteSpace(m.Nome)) atual.Nome = m.Nome;

                await _context.SaveChangesAsync();
                return (true, "Marca atualizada com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar a marca. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirMarca(int id)
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id);
                if (marca == null)
                    return (false, "Marca não encontrada.");

                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
                return (true, "Marca excluída com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir esta marca pois ela está vinculada a produtos.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
