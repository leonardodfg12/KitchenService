using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KitchenService.Domain.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!; // ex: "Retirada", "Entrega"
    public List<OrderItem> Items { get; set; } = new();
    public string Status { get; private set; } = "Pendente";
    public string? Justificativa { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}