using KitchenService.Domain.Entities;
using KitchenService.Domain.Interfaces;
using MongoDB.Driver;

namespace KitchenService.Infrastructure.Persistence;

public class MongoKitchenDecisionRepository : IKitchenDecisionRepository
{
    private readonly IMongoCollection<KitchenDecision> _collection;

    public MongoKitchenDecisionRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<KitchenDecision>("KitchenDecisions");
    }

    public async Task AddAsync(KitchenDecision decision)
    {
        await _collection.InsertOneAsync(decision);
    }
}