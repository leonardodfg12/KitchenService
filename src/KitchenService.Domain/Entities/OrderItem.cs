namespace KitchenService.Domain.Entities;

public class OrderItem
{
    public string MenuItemId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal Total => Price * Quantity;
}