using KitchenService.Domain.Entities;
using MongoDB.Driver;

namespace KitchenService.Infrastructure.Persistence;

public class MongoOrderUpdater(IMongoDatabase database)
{
    private readonly IMongoCollection<Order> _collection = database.GetCollection<Order>("Orders");

    public async Task UpdateOrderStatusAsync(string orderId, string newStatus, string? justification = null)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
        var update = Builders<Order>.Update
            .Set(x => x.Status, newStatus)
            .Set(x => x.Justification, justification)
            .Set(x => x.CreatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}