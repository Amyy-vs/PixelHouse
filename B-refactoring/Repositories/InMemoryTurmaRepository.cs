using TurmasApi.DTOs;
using TurmasApi.Models;

namespace TurmasApi.Repositories;

public class InMemoryTurmaRepository : ITurmaRepository
{
    private readonly List<Turma> _turmas = new()
    {
        new Turma { IdTurma = 1, IdEmpresa = 10, NomeTurma = "Engenharia Civil 2024", AnoFormatura = 2024, Status = "ATIVA"     },
        new Turma { IdTurma = 2, IdEmpresa = 10, NomeTurma = "Medicina 2023",          AnoFormatura = 2023, Status = "ENCERRADA" },
        new Turma { IdTurma = 3, IdEmpresa = 11, NomeTurma = "Direito 2025",           AnoFormatura = 2025, Status = "ATIVA"     },
    };

    private readonly List<Pedido> _pedidos;

    public InMemoryTurmaRepository()
    {
        _pedidos = new List<Pedido>();

        var id = 100;
        for (var i = 0; i < 30; i++)
            _pedidos.Add(new Pedido { IdPedido = id++, IdTurma = 1, Produto = "ALBUM_PREMIUM", Valor = 450.00m, DataPedido = DateTime.Today, Status = "APROVADO" });
        for (var i = 0; i < 13; i++)
            _pedidos.Add(new Pedido { IdPedido = id++, IdTurma = 1, Produto = "ALBUM_PADRAO",  Valor = 250.00m, DataPedido = DateTime.Today, Status = "APROVADO" });
        for (var i = 0; i < 4; i++)
            _pedidos.Add(new Pedido { IdPedido = id++, IdTurma = 1, Produto = "FOTO_AVULSA",   Valor = 500.00m, DataPedido = DateTime.Today, Status = "APROVADO" });

        // Pedido cancelado - não deve ser considerado  
        _pedidos.Add(new Pedido { IdPedido = id++, IdTurma = 1, Produto = "FOTO_AVULSA", Valor = 50.00m, DataPedido = DateTime.Today, Status = "CANCELADO" });

        // Pedidos de outras turmas
        _pedidos.Add(new Pedido { IdPedido = id++, IdTurma = 2, Produto = "ALBUM_PADRAO",  Valor = 300.00m, DataPedido = DateTime.Today, Status = "APROVADO" });
        _pedidos.Add(new Pedido { IdPedido = id++, IdTurma = 3, Produto = "ALBUM_PREMIUM", Valor = 500.00m, DataPedido = DateTime.Today, Status = "APROVADO" });
    }

    // -----------------------------------------------------------------------
    // Métodos
    // -----------------------------------------------------------------------

    public Task<IEnumerable<PedidoDto>> GetPedidosAprovadosAsync(int idTurma)
    {
        var result = _pedidos
            .Where(p => p.IdTurma == idTurma && p.Status == "APROVADO")
            .Select(p => new PedidoDto
            {
                IdPedido   = p.IdPedido,
                Produto    = p.Produto,
                Valor      = p.Valor,
                DataPedido = p.DataPedido
            });

        return Task.FromResult(result);
    }

    public Task<int> CriarTurmaAsync(CriarTurmaDto dto)
    {
        var novoId = _turmas.Max(t => t.IdTurma) + 1;
        _turmas.Add(new Turma
        {
            IdTurma      = novoId,
            IdEmpresa    = dto.IdEmpresa,
            NomeTurma    = dto.NomeTurma,
            AnoFormatura = dto.AnoFormatura,
            Status       = "ATIVA"
        });
        return Task.FromResult(novoId);
    }

    public Task<TurmaDetalheDto?> GetTurmaDetalheAsync(int idTurma)
    {
        var turma = _turmas.FirstOrDefault(t => t.IdTurma == idTurma);

        if (turma is null)
            return Task.FromResult<TurmaDetalheDto?>(null);

        var pedidosAprovados = _pedidos
            .Where(p => p.IdTurma == idTurma && p.Status == "APROVADO")
            .ToList();

        var dto = new TurmaDetalheDto
        {
            IdTurma               = turma.IdTurma,
            NomeTurma             = turma.NomeTurma,
            AnoFormatura          = turma.AnoFormatura,
            Status                = turma.Status,
            TotalPedidosAprovados = pedidosAprovados.Count,
            ReceitaTotal          = pedidosAprovados.Sum(p => p.Valor)
        };

        return Task.FromResult<TurmaDetalheDto?>(dto);
    }
}
