using TurmasApi.DTOs;

namespace TurmasApi.Repositories;

public interface ITurmaRepository
{
    Task<IEnumerable<PedidoDto>> GetPedidosAprovadosAsync(int idTurma);
    Task<int> CriarTurmaAsync(CriarTurmaDto dto);   
    Task<TurmaDetalheDto?> GetTurmaDetalheAsync(int idTurma);
}
