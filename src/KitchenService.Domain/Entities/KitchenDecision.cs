namespace KitchenService.Domain.Entities;

public class KitchenDecision
{
    public string OrderId { get; set; } = default!;
    public string Decision { get; set; } = default!; // "Aceito" ou "Rejeitado"
    public string? Justification { get; set; }       // Apenas em caso de rejeição
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}