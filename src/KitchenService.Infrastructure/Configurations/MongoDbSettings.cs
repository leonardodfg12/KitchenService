using System.Diagnostics.CodeAnalysis;

namespace KitchenService.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}