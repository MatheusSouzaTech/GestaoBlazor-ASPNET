using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelsLibrary.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _service;
        private readonly IConfiguration _config;

        public AuthController(UsuarioService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuarios = await _service.Listar();
            var usuario = usuarios.FirstOrDefault(u =>
                u.Login.Equals(request.Login, StringComparison.OrdinalIgnoreCase) && u.Ativo);

            if (usuario == null)
                return Unauthorized(new { mensagem = "Login ou senha inválidos." });

            bool senhaValida;
            try { senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash); }
            catch { senhaValida = false; }

            if (!senhaValida)
                return Unauthorized(new { mensagem = "Login ou senha inválidos." });

            await _service.RegistrarUltimoLogin(usuario.Id);

            var token = GerarToken(usuario);
            return Ok(new LoginResponse
            {
                Token = token,
                Nome = usuario.Nome,
                Login = usuario.Login,
                Id = usuario.Id
            });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequest request)
        {
            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Login = request.Login,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                Telefone = request.Telefone,
                CPF = request.CPF,
                Ativo = true,
                DataCadastro = DateTime.Now
            };

            var (sucesso, mensagem) = await _service.Adicionar(usuario);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        private string GerarToken(Usuario usuario)
        {
            var jwtSection = _config.GetSection("Jwt");
            var secretKey = jwtSection["SecretKey"]!;
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expHoras = int.Parse(jwtSection["ExpiracaoHoras"] ?? "8");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expHoras),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
