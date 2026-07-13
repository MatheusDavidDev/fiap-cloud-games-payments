using FCG.Payments.Core.Models;

namespace FCG.Payments.Domain.Entities;

public class Pagamento : BaseEntity
{
    public Pagamento(Guid idOrdemCompra, Guid idUsuario, Guid idJogo, decimal preco)
    {
        IdOrdemCompra = idOrdemCompra;
        IdUsuario = idUsuario;
        IdJogo = idJogo;
        Preco = preco;
        Status = StatusPagamento.Pentende;
    }

    public Guid IdOrdemCompra{ get; private set; }
    public Guid IdUsuario { get; private set; }
    public Guid IdJogo { get; private set; }
    public decimal Preco { get; private set; }
    public StatusPagamento Status { get; private set; }
    public DateTime ProcessedAt { get; private set; }

    public void Aprovar()
    {
        Status = StatusPagamento.Aprovado;
        ProcessedAt = DateTime.UtcNow;
    }

    public void Rejeitar()
    {
        Status = StatusPagamento.Rejeitado;
        ProcessedAt = DateTime.UtcNow;
    }
}
