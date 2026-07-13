namespace FCG.Payments.Application.Queries.DTOs;

public class PagamentoDto
{

    public Guid Id { get; set; }
    public Guid IdUsuario { get; set; }
    public Guid IdJogo { get; set; }
    public decimal Preco { get; set; }
    public string Status { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string ProcessadoEm => ProcessedAt?.ToString("dd/MM/yyyy") ?? string.Empty;
}
