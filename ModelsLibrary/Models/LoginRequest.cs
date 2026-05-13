using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O login é obrigatório.")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Nome  { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public int    Id    { get; set; }
    }

    public class RegistroRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O login é obrigatório.")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme a senha.")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        public string? Telefone { get; set; }
        public string? CPF      { get; set; }
    }

    /// <summary>
    /// DTO usado pelo formulário de edição de usuário.
    /// SenhaHash é opcional: vazio = manter a senha atual.
    /// </summary>
    public class UsuarioEditDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O login é obrigatório.")]
        public string Login { get; set; } = string.Empty;

        /// <summary>Deixar em branco para manter a senha atual.</summary>
        [MinLength(6, ErrorMessage = "A nova senha deve ter pelo menos 6 caracteres.")]
        public string? NovaSenha { get; set; }

        public string? Telefone { get; set; }
        public string? CPF      { get; set; }
        public bool    Ativo    { get; set; } = true;
    }
}

