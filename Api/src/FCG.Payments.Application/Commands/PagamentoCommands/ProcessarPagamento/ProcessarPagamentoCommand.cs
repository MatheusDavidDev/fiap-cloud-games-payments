using MediatR;

namespace FCG.Payments.Application.Commands.PagamentoCommands.ProcessarPagamento;

public class ProcessarPagamentoCommand : IRequest
{
    public ProcessarPagamentoCommand(Guid idUsuario, Guid idJogo, decimal preco)
    {
        IdUsuario = idUsuario;
        IdJogo = idJogo;
        Preco = preco;
    }

    public Guid IdUsuario { get; set; }
    public Guid IdJogo { get; set; }
    public decimal Preco { get; set; }
}
