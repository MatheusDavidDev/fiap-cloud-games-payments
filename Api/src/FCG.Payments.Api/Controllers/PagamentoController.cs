using FCG.Payments.Api.Controllers.Models;
using FCG.Payments.Application.Commands.PagamentoCommands.ProcessarPagamento;
using FCG.Payments.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Payments.Api.Controllers;

[ApiController]
[Route("api/pagamentos")]
public class PagamentoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPagamentoQueryService _queryService;

    public PagamentoController(IMediator mediator, IPagamentoQueryService queryService)
    {
        _mediator = mediator;
        _queryService = queryService;
    }

    [HttpPost("processar-pagamento")]
    public async Task<IActionResult> ProcessarPagamento(ProcessarPagamentoModel model)
    {
        await _mediator.Send(new ProcessarPagamentoCommand(model.IdOrdemCompra, model.IdUsuario, model.IdJogo, model.Preco));
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPagamentoPorId(Guid id)
    {
        var result = await _queryService.ObterPagamentoPorIdAsync(id, CancellationToken.None);
        return Ok(result);
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<IActionResult> ObterPagamentosPorIdUsuario(Guid idUsuario)
    {
        var result = await _queryService.ObterPagamentosPorIdUsuarioAsync(idUsuario, CancellationToken.None);
        return Ok(result);
    }
}
