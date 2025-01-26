using ECommerce.Inventory.ApplicationUseCases.Behaviors;
using ECommerce.Inventory.ApplicationUseCases.CommandsAndQueries;
using ECommerce.Inventory.ApplicationUseCases.IntegrationEvents;
using ECommerce.Inventory.ApplicationUseCases.Validators;
using ECommerce.Inventory.Domain;
using ECommerce.Inventory.DrivenAdapters.Persistence.SqlDatabase;
using ECommerce.SharedFramework;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.DrivingAdapters.RestApi;

public static class WireUpExtension
{
    public static void RegisterDatabaseServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDatabase"));
        });
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProductStockRepository, ProductStockRepository>();
    }

    public static void RegisterMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
    }

    public static void RegisterFluentValidator(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IValidator<AddStockCommand>, AddStockCommandValidator>();
        builder.Services.AddSingleton<IValidator<EditStockCommand>, EditStockCommandValidator>();
    }

    public static void RegisterMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.UsingRabbitMq((_, cfg) =>
            {
                cfg.Host(builder.Configuration.GetConnectionString("rabbitmq"));
                cfg.UseRawJsonSerializer();

                cfg.Message<ProductStockCreatedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductStockCreatedEvent));
                });
                cfg.Message<ProductStockEditedEvent>(conf => { conf.SetEntityName(nameof(ProductStockEditedEvent)); });
            });
        });
    }
}