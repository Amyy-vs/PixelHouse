-- ============================================================
-- Descrição: Consolida os pedidos aprovados do mês corrente
--            por empresa na tabela Relatorio_VendasMensal.
-- ============================================================

CREATE OR ALTER PROCEDURE sp_ConsolidarVendasMensais
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MesReferencia DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);

    INSERT INTO Relatorio_VendasMensal
        (IdEmpresa, MesReferencia, RazaoSocial, TotalPedidos, ReceitaTotal, DataCarga)
    SELECT
        e.IdEmpresa,
        @MesReferencia                  AS MesReferencia,
        e.RazaoSocial,
        COUNT(p.IdPedido)               AS TotalPedidos,
        ISNULL(SUM(p.Valor), 0)         AS ReceitaTotal,
        GETDATE()                       AS DataCarga
    FROM Empresas e
    INNER JOIN Turmas t
        ON t.IdEmpresa = e.IdEmpresa
        AND t.Status   = 'ATIVA'          
    INNER JOIN Pedidos p
        ON p.IdTurma    = t.IdTurma
        AND p.Status    = 'APROVADO'     
        AND p.DataPedido >= @MesReferencia 
        AND p.DataPedido <  DATEADD(MONTH, 1, @MesReferencia)
    WHERE e.Status = 'ATIVO'              
      AND NOT EXISTS (                    
            SELECT 1
            FROM Relatorio_VendasMensal r
            WHERE r.IdEmpresa     = e.IdEmpresa
              AND r.MesReferencia = @MesReferencia
          )
    GROUP BY e.IdEmpresa, e.RazaoSocial;
END;
GO
