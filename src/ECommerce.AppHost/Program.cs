using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
        .WithDataVolume(isReadOnly: false)
        .WithManagementPlugin();

var seq = builder.AddSeq("seq")
    .ExcludeFromManifest()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithEnvironment("ACCEPT_EULA", "Y");

var redisCache = builder.AddRedis("redis-cache")
    .WithLifetime(ContainerLifetime.Persistent);

var sqlServerDatabase = builder.AddSqlServer("sql-server")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("ECommerceDatabase");
   
var productManagement = builder.AddProject<Projects.ECommerce_ProductManagement>("product-management")
    .WithReference(sqlServerDatabase)
    .WaitFor(sqlServerDatabase)
    .WithReference(redisCache)
    .WaitFor(redisCache)
    .WithReference(seq)
    .WaitFor(seq)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder.AddProject<Projects.ECommerce_Gateway>("gateway")
    .WithExternalHttpEndpoints()
    .WithReference(productManagement)
    .WaitFor(productManagement)
    .WithReference(seq)
    .WaitFor(seq);

builder.Build().Run();
