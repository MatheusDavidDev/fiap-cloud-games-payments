using MediatR;

namespace FCG.Payments.Application.Commands.PagamentoCommands.ProcessarPagamento;

public class ProcessarPagamentoCommand : IRequest
{
    public ProcessarPagamentoCommand(Guid idOrdemCompra, Guid idUsuario, Guid idJogo, decimal preco)
    {
        IdOrdemCompra = idOrdemCompra;
        IdUsuario = idUsuario;
        IdJogo = idJogo;
        Preco = preco;
    }

    public Guid IdOrdemCompra { get; private set; }
    public Guid IdUsuario { get; set; }
    public Guid IdJogo { get; set; }
    public decimal Preco { get; set; }
}
