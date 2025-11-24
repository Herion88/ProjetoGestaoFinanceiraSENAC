using Microsoft.AspNetCore.Mvc;
using ProjetoGestao.Api.Data;
using ProjetoGestao.Api.Models;
using using Microsoft.AspNetCore.Mvc;
using ProjetoGestao.Api.Data;
using ProjetoGestao.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoGestao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LancamentosController(AppDbContext context)
        {
            _context = context;
        }

        // --- 2. MÉTODO DE CRIAÇÃO ---
        // POST: api/lancamentos (Registra uma nova receita ou despesa)
        [HttpPost]
        public async Task<IActionResult> CriarLancamento([FromBody] Lancamento novoLancamento)
        {
            // Validações básicas
            if (novoLancamento == null || novoLancamento.Valor <= 0 || string.IsNullOrWhiteSpace(novoLancamento.Descricao))
            {
                return BadRequest("Dados do lançamento inválidos.");
            }

            // Verifica se o usuário do lançamento existe
            var usuario = await _context.Usuarios.FindAsync(novoLancamento.UsuarioId);
            if (usuario == null)
            {
                return BadRequest("Usuário (UsuarioId) não encontrado.");
            }

            try
            {
                _context.Lancamentos.Add(novoLancamento);
                await _context.SaveChangesAsync();
                
                // Retorna o objeto criado, que será usado para atualizar a tela
                return Ok(novoLancamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // --- 3. MÉTODO DE BUSCA ---
        // GET: api/lancamentos/por-usuario/{usuarioId}
        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<IActionResult> GetLancamentosPorUsuario(int usuarioId)
        {
            // Verifica se o usuário existe
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Busca todos os lançamentos daquele usuário no banco
            var lancamentos = await _context.Lancamentos
                                        .Where(l => l.UsuarioId == usuarioId)
                                        .OrderBy(l => l.Data) // Ordena por data
                                        .ToListAsync(); // Converte em uma lista

            // Retorna a lista.
            // Se não houver lançamentos, ele retornará '[]' (um array JSON vazio),
            // o que é VÁLIDO e não quebrará o 'response.json()'.
            return Ok(lancamentos);
        }
    }
}
