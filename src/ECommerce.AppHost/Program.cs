using Aspire.Hosting;
namespace ECommerce.AppHost;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var rabbitmq = builder.AddRabbitMQ("rabbitmq")
            .WithDataVolume(isReadOnly: false)
            .WithManagementPlugin();

        var seq = builder.AddSeq("seq")
            .ExcludeFromManifest()
            .WithDataVolume(isReadOnly: false)
            .WithEnvironment("ACCEPT_EULA", "Y");

        var redisCache = builder.AddRedis("redis-cache")
            .WithDataVolume(isReadOnly: false);

        var sqlServerDatabase = builder.AddSqlServer("sql-server")
            .WithDataVolume(isReadOnly: false)
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
    }
}
