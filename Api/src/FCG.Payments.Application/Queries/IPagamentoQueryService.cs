using FCG.Payments.Application.Queries.DTOs;

namespace FCG.Payments.Application.Queries;

public interface IPagamentoQueryService
{
    Task<PagamentoDto> ObterPagamentoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<PagamentoDto>> ObterPagamentosPorIdUsuarioAsync(Guid id, CancellationToken cancellationToken);

}
