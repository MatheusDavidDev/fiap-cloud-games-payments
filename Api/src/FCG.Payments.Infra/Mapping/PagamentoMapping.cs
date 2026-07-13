using FCG.Payments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Payments.Infra.Mapping;

public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.ToTable("Pagamentos");

        builder.Property(x => x.IdUsuario)
            .IsRequired();

        builder.Property(x => x.IdJogo)
            .IsRequired();

        builder.Property(x => x.Preco)
            .HasPrecision(10, 2);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();
    }
}
