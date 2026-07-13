using FCG.Payments.Application.Commands.PagamentoCommands.ProcessarPagamento;
using FCG.Payments.Application.Queries;
using FCG.Payments.Core.Behaviors;
using FCG.Payments.Core.UnitOfWork;
using FCG.Payments.Infra;
using FCG.Payments.Infra.Queries;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FcgPaymentsDbContext>(options => options.UseSqlServer(connectionString));


#region DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<FcgPaymentsDbContext>>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Query Services
builder.Services.AddScoped<IPagamentoQueryService, PagamentoQueryService>();

#endregion

#region MEDIATOR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessarPagamentoHandler).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(ProcessarPagamentoValidator).Assembly);
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FcgPaymentsDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
