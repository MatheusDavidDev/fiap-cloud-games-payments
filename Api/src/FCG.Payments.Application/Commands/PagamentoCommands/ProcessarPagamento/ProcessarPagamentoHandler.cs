using FCG.Payments.Core.UnitOfWork;
using FCG.Payments.Domain.Entities;
using MediatR;

namespace FCG.Payments.Application.Commands.PagamentoCommands.ProcessarPagamento;

public class ProcessarPagamentoHandler : IRequestHandler<ProcessarPagamentoCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProcessarPagamentoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProcessarPagamentoCommand request, CancellationToken cancellationToken)
    {
        var pagamentoRepository = _unitOfWork.GetRepository<Pagamento>();

        var pagamentoExistente = await pagamentoRepository.GetByAsync(
            predicate: u => u.IdUsuario == request.IdUsuario && u.IdJogo == request.IdJogo,
            cancellationToken: cancellationToken);

        if (pagamentoExistente != null && pagamentoExistente.Status == StatusPagamento.Pentende)
        {
            throw new Exception("O usuario já possui um processamento pendente.");
        }

        var pagamento = new Pagamento(
            request.IdUsuario,
            request.IdJogo,
            request.Preco);

        if (request.Preco > 100)
        {
            pagamento.Rejeitar();
            pagamento.SetUpdated();
        }
        else
        {
            pagamento.Aprovar();
            pagamento.SetUpdated();
        }

        await pagamentoRepository.AddAsync(pagamento, cancellationToken);

        await _unitOfWork.SaveChanges();
    }
}
