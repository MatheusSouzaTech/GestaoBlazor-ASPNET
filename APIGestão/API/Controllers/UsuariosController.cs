using APIGestão.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuariosController(UsuarioService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.Listar());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var u = await _service.BuscarId(id);
            return u == null ? NotFound() : Ok(u);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (!string.IsNullOrWhiteSpace(usuario.SenhaHash))
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);

            var (sucesso, mensagem) = await _service.Adicionar(usuario);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioEditDto dto)
        {
            var existente = await _service.BuscarId(id);
            if (existente == null) return NotFound(new { mensagem = "Usuário não encontrado." });

            existente.Nome     = dto.Nome;
            existente.Email    = dto.Email;
            existente.Login    = dto.Login;
            existente.Telefone = dto.Telefone;
            existente.CPF      = dto.CPF;
            existente.Ativo    = dto.Ativo;

            if (!string.IsNullOrWhiteSpace(dto.NovaSenha))
                existente.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);

            var (sucesso, mensagem) = await _service.Atualizar(id, existente);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (sucesso, mensagem) = await _service.Excluir(id);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }
    }
}
