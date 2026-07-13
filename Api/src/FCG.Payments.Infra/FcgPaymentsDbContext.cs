using FCG.Payments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG.Payments.Infra;

public class FcgPaymentsDbContext : DbContext
{
    public FcgPaymentsDbContext(DbContextOptions<FcgPaymentsDbContext> options) : base(options)
    {
    }
    public DbSet<Pagamento> Pagamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FcgPaymentsDbContext).Assembly);
    }
}
