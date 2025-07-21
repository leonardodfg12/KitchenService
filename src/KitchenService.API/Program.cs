using System.Diagnostics.CodeAnalysis;
using KitchenService.Application.Handlers;
using KitchenService.Domain.Interfaces;
using KitchenService.Infrastructure.Configurations;
using KitchenService.Infrastructure.Persistence;
using MassTransit;
using MongoDB.Driver;

namespace KitchenService.API;

[ExcludeFromCodeCoverage]
public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddMassTransit(
            cfg =>
            {
                cfg.AddConsumer<KitchenServiceHandler>();
                cfg.UsingRabbitMq((context, config) =>
                {
                    config.Host(builder.Configuration["RABBITMQ_HOST"], h =>
                    {
                        h.Username(builder.Configuration["RABBITMQ_USERNAME"]!);
                        h.Password(builder.Configuration["RABBITMQ_PASSWORD"]!);
                    });
                    config.ReceiveEndpoint("order-created",
                        e => { e.ConfigureConsumer<KitchenServiceHandler>(context); });
                });
            });

        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = builder.Configuration["MONGO_CONNECTION_STRING"];
            return new MongoClient(connectionString);
        });

        builder.Services.AddScoped(sp =>
        {
            var databaseName = builder.Configuration["MONGO_DATABASE_NAME"];
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        builder.Services.AddScoped<IKitchenDecisionRepository, MongoKitchenDecisionRepository>();
        builder.Services.AddScoped<KitchenServiceHandler>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}