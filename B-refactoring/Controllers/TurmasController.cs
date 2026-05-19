using Microsoft.AspNetCore.Mvc;
using TurmasApi.DTOs;
using TurmasApi.Repositories;

namespace TurmasApi.Controllers;

[ApiController]
[Route("api/turmas")]
public class TurmasController : ControllerBase
{
    private readonly ITurmaRepository _repository;

    public TurmasController(ITurmaRepository repository)
    {
        _repository = repository;
    }

    // ------------------------------------------------------------------
    // GET /api/turmas/{id}/pedidos
    // Retorna os pedidos aprovados de uma turma.
    // ------------------------------------------------------------------
    [HttpGet("{id}/pedidos")]
    public async Task<IActionResult> GetPedidos(int id)
    {
        var pedidos = await _repository.GetPedidosAprovadosAsync(id);
        return Ok(pedidos);
    }

    // ------------------------------------------------------------------
    // GET /api/turmas/{id}
    // Retorna detalhes da turma + totalizadores de pedidos aprovados.
    // 404 se a turma não existir.
    // ------------------------------------------------------------------
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTurma(int id)
    {
        var turma = await _repository.GetTurmaDetalheAsync(id);

        if (turma is null)
            return NotFound();

        return Ok(turma);
    }

    // ------------------------------------------------------------------
    // POST /api/turmas
    // Cria uma nova turma com status ATIVA.
    // Retorna 201 Created com o Location do novo recurso.
    // ModelState inválido é rejeitado automaticamente pelo [ApiController].
    // ------------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> CriarTurma([FromBody] CriarTurmaDto dto)
    {
        var idTurma = await _repository.CriarTurmaAsync(dto);
        return CreatedAtAction(nameof(GetTurma), new { id = idTurma }, null);
    }
}
