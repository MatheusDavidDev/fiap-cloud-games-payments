using FCG.Payments.Application.Queries;
using FCG.Payments.Application.Queries.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FCG.Payments.Infra.Queries;

public class PagamentoQueryService : IPagamentoQueryService
{
    private readonly FcgPaymentsDbContext _context;

    public PagamentoQueryService(FcgPaymentsDbContext context)
    {
        _context = context;
    }

    public async Task<PagamentoDto> ObterPagamentoPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var pagamento = await _context.Pagamentos
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return new PagamentoDto
        {
            Id = pagamento.Id,
            IdUsuario = pagamento.IdUsuario,
            IdJogo = pagamento.IdJogo,
            Preco = pagamento.Preco,
            Status = pagamento.Status.ToString(),
            ProcessedAt = pagamento.ProcessedAt
        };
    }

    public async Task<IEnumerable<PagamentoDto>> ObterPagamentosPorIdUsuarioAsync(Guid id, CancellationToken cancellationToken)
    {
        var pagamento = await _context.Pagamentos
            .Where(x => x.IdUsuario == id)
            .ToListAsync(cancellationToken);

        return pagamento.OrderByDescending(x => x.ProcessedAt).Select(p => new PagamentoDto
        {
            Id = p.Id,
            IdUsuario = p.IdUsuario,
            IdJogo = p.IdJogo,
            Preco = p.Preco,
            Status = p.Status.ToString(),
            ProcessedAt = p.ProcessedAt
        });
    }
}
