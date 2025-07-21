namespace KitchenService.Application.Contracts;

public class OrderStatusChangedEvent
{
    public string Id { get; set; } = default!;
    public string NewStatus { get; set; } = default!; // "Aceito" ou "Rejeitado"
    public string? Justification { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}