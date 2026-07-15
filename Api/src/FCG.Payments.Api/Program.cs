using FCG.Payments.Api.Erros;
using FCG.Payments.Api.Middlewares;
using FCG.Payments.Application.Commands.PagamentoCommands.ProcessarPagamento;
using FCG.Payments.Application.Queries;
using FCG.Payments.Core.Behaviors;
using FCG.Payments.Core.UnitOfWork;
using FCG.Payments.Infra;
using FCG.Payments.Infra.Queries;
using FCG.Payments.Infra.Rabbitmq.Consumers;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FcgPaymentsDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderPlacedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            builder.Configuration["RabbitMQ:Host"],
            builder.Configuration["RabbitMQ:VirtualHost"],
            h =>
            {
                h.Username(builder.Configuration["RabbitMQ:Username"!]);
                h.Password(builder.Configuration["RabbitMQ:Password"]);
            });

        cfg.ConfigureEndpoints(context);
    });
});


#region DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<FcgPaymentsDbContext>>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
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
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.MapScalarApiReference();
//}

app.MapOpenApi();
app.MapScalarApiReference();

if (System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider")
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<FcgPaymentsDbContext>();
        db.Database.Migrate();
    }
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
