namespace FCG.Payments.Api.Controllers.Models;

public record ProcessarPagamentoModel(Guid IdOrdemCompra, Guid IdUsuario, Guid IdJogo, decimal Preco);


