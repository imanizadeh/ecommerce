using EasyCaching.Core.Configurations;
using ECommerce.Contracts.IntegrationEvents.ProductManagement;
using ECommerce.ProductManagement.ApplicationUseCases.Behaviors;
using ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;
using ECommerce.ProductManagement.ApplicationUseCases.Common;
using ECommerce.ProductManagement.ApplicationUseCases.Validators;
using ECommerce.ProductManagement.Domain.ProductCategories;
using ECommerce.ProductManagement.Domain.Products;
using ECommerce.ProductManagement.DrivenAdapters.Persistence.Cache;
using ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase;
using ECommerce.SharedFramework;
using FluentValidation;
using MassTransit;
using MessagePack.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.ProductManagement.DrivingAdapters.GrpcApi;

public static class WireUpExtension
{
    public static void RegisterDatabaseServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDatabase"));
        });
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
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
        builder.Services.AddSingleton<IValidator<AddProductCategoryCommand>, AddProductCategoryCommandValidator>();
        builder.Services.AddSingleton<IValidator<AddProductCommand>, AddProductCommandValidator>();
        builder.Services.AddSingleton<IValidator<AddProductSpecificationCommand>, AddProductSpecificationCommandValidator>();
        builder.Services.AddSingleton<IValidator<EditProductCategoryCommand>, EditProductCategoryCommandValidator>();
        builder.Services.AddSingleton<IValidator<EditProductCommand>, EditProductCommandValidator>();
        builder.Services.AddSingleton<IValidator<EditProductSpecificationCommand>, EditProductSpecificationCommandValidator>();
    }

    public static void RegisterMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseRawJsonSerializer();
                cfg.Host(builder.Configuration.GetConnectionString("rabbitmq"));
                cfg.ConfigureEndpoints(context);
                
                cfg.Message<ProductCategoryCreatedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductCategoryCreatedEvent));
                });
                cfg.Message<ProductCategoryDeletedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductCategoryDeletedEvent));
                });
                cfg.Message<ProductCategoryEditedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductCategoryEditedEvent));
                }); 
                cfg.Message<ProductCreatedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductCreatedEvent));
                });
                cfg.Message<ProductDeletedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductDeletedEvent));
                });     
                cfg.Message<ProductEditedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductEditedEvent));
                });
                cfg.Message<ProductSpecCreatedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductSpecCreatedEvent));
                });  
                cfg.Message<ProductSpecDeletedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductSpecDeletedEvent));
                });
                cfg.Message<ProductSpecEditedEvent>(conf =>
                {
                    conf.SetEntityName(nameof(ProductSpecEditedEvent));
                });
            });
        });
    }
    
    public static void RegisterCachingService(this WebApplicationBuilder builder)
    {
        
        var redisUrl = builder.Configuration.GetConnectionString("redis-cache");
        builder.Services.AddEasyCaching(option =>
        {
            option.UseRedis(config => 
            {
                config.DBConfig.Endpoints.Add(new ServerEndPoint(redisUrl.Split(":")[0], int.Parse(redisUrl.Split(":")[1])));
                config.SerializerName = "MessagePack";
            }).WithMessagePack(options =>
            {
                options.EnableCustomResolver = true;
                options.CustomResolvers = CompositeResolver.Create(
                    NativeDateTimeResolver.Instance,
                    ContractlessStandardResolver.Instance);
            },"MessagePack");
        });
        builder.Services.AddTransient<ICacheService, CacheService>();
    }
    

}