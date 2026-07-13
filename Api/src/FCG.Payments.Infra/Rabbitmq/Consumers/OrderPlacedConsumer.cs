using FCG.Contracts;
using FCG.Payments.Application.Commands.PagamentoCommands.ProcessarPagamento;
using MassTransit;
using MediatR;

namespace FCG.Payments.Infra.Rabbitmq.Consumers;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly IMediator _mediator;


    public OrderPlacedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var ordem = context.Message;

        Console.WriteLine("Evento recebido!");

        await _mediator.Send(new ProcessarPagamentoCommand(ordem.IdOrdemCompra, ordem.IdUsuario, ordem.IdJogo, ordem.Preco));
    }
}
