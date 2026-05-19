using System.ComponentModel.DataAnnotations;

namespace TurmasApi.DTOs;

// ---------- Entrada ----------

public class CriarTurmaDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "IdEmpresa deve ser maior que zero.")]
    public int IdEmpresa { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "NomeTurma deve ter entre 3 e 200 caracteres.")]
    public string NomeTurma { get; set; } = string.Empty;

    [Required]
    [Range(2000, 2100, ErrorMessage = "AnoFormatura deve estar entre 2000 e 2100.")]
    public int AnoFormatura { get; set; }
}

// ---------- Saídas ----------

public class PedidoDto
{
    public int      IdPedido   { get; set; }
    public string   Produto    { get; set; } = string.Empty;
    public decimal  Valor      { get; set; }
    public DateTime DataPedido { get; set; }
}

public class TurmaDetalheDto
{
    public int     IdTurma               { get; set; }
    public string  NomeTurma             { get; set; } = string.Empty;
    public int     AnoFormatura          { get; set; }
    public string  Status                { get; set; } = string.Empty;
    public int     TotalPedidosAprovados { get; set; }
    public decimal ReceitaTotal          { get; set; }
}
