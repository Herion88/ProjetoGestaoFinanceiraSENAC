using Microsoft.AspNetCore.Mvc;
using ProjetoGestao.Api.Data;
using ProjetoGestao.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoGestao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/usuarios
        // Este método vai "fazer login" ou "cadastrar"
        [HttpPost]
        public async Task<IActionResult> LoginOuCadastrarUsuario([FromBody] Usuario dadosUsuario)
        {
            // Validação básica
            if (dadosUsuario == null || string.IsNullOrWhiteSpace(dadosUsuario.Email))
            {
                return BadRequest("Email é obrigatório.");
            }

            // 1. Tenta encontrar o usuário pelo email (ignorando maiúsculas/minúsculas)
            var usuarioExistente = await _context.Usuarios
                                        .FirstOrDefaultAsync(u => u.Email.ToLower() == dadosUsuario.Email.ToLower());

            // 2. SE O USUÁRIO EXISTE (LOGIN): Retorna o usuário encontrado
            if (usuarioExistente != null)
            {
                // O frontend vai receber o ID existente e carregar os dados corretos
                return Ok(usuarioExistente);
            }

            // 3. SE O USUÁRIO NÃO EXISTE (CADASTRO):
            // Valida se o nome foi enviado, já que é um novo cadastro
            if (string.IsNullOrWhiteSpace(dadosUsuario.Nome))
            {
                return BadRequest("Nome é obrigatório para novos cadastros.");
            }
            
            // Salva o novo usuário no banco
            _context.Usuarios.Add(dadosUsuario);
            await _context.SaveChangesAsync();

            // Retorna o usuário que acabou de ser criado (com o novo ID)
            return Ok(dadosUsuario);
        }
    }
}
