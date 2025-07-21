namespace OrderService.Application.Contracts; // <- MESMO namespace do OrderService

public class OrderCreatedEvent
{
    public string Id { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public string DeliveryMethod { get; set; } = default!;
}