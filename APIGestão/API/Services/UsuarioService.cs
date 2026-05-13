using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace APIGestão.API.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context) => _context = context;

        public async Task<List<Usuario>> Listar() =>
            await _context.Usuarios.ToListAsync();

        public async Task<Usuario?> BuscarId(int id) =>
            await _context.Usuarios.FindAsync(id);

        public async Task<(bool sucesso, string mensagem)> Adicionar(Usuario u)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(u.Nome))
                    return (false, "O nome do usuário é obrigatório.");
                if (string.IsNullOrWhiteSpace(u.Email))
                    return (false, "O e-mail do usuário é obrigatório.");
                if (string.IsNullOrWhiteSpace(u.Login))
                    return (false, "O login do usuário é obrigatório.");
                if (string.IsNullOrWhiteSpace(u.SenhaHash))
                    return (false, "A senha do usuário é obrigatória.");

                var emailExiste = await _context.Usuarios.AnyAsync(x => x.Email == u.Email);
                if (emailExiste)
                    return (false, "Já existe um usuário cadastrado com este e-mail.");

                var loginExiste = await _context.Usuarios.AnyAsync(x => x.Login == u.Login);
                if (loginExiste)
                    return (false, "Já existe um usuário cadastrado com este login.");

                _context.Usuarios.Add(u);
                await _context.SaveChangesAsync();
                return (true, "Usuário cadastrado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao salvar o usuário. Verifique se o e-mail ou login já estão em uso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> Atualizar(int id, Usuario u)
        {
            try
            {
                var atual = await _context.Usuarios.FindAsync(id);
                if (atual == null)
                    return (false, "Usuário não encontrado.");

                if (!string.IsNullOrWhiteSpace(u.Nome)) atual.Nome = u.Nome;
                if (!string.IsNullOrWhiteSpace(u.Email)) atual.Email = u.Email;
                if (!string.IsNullOrWhiteSpace(u.Login)) atual.Login = u.Login;
                if (!string.IsNullOrWhiteSpace(u.SenhaHash)) atual.SenhaHash = u.SenhaHash;
                if (!string.IsNullOrWhiteSpace(u.Telefone)) atual.Telefone = u.Telefone;
                if (!string.IsNullOrWhiteSpace(u.CPF)) atual.CPF = u.CPF;
                if (u.IDUsuarioLider.HasValue) atual.IDUsuarioLider = u.IDUsuarioLider;
                atual.Ativo = u.Ativo;

                await _context.SaveChangesAsync();
                return (true, "Usuário atualizado com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Erro ao atualizar o usuário. Verifique se o e-mail ou login já estão em uso.");
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
                var u = await _context.Usuarios.FindAsync(id);
                if (u == null)
                    return (false, "Usuário não encontrado.");

                _context.Usuarios.Remove(u);
                await _context.SaveChangesAsync();
                return (true, "Usuário excluído com sucesso.");
            }
            catch (DbUpdateException)
            {
                return (false, "Não é possível excluir este usuário pois ele está vinculado a outros registros.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
