using Microsoft.AspNetCore.Mvc;
using KitchenService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using OrderService.Application.Contracts;

namespace KitchenService.API.Controllers;

[ApiController]
[Route("kitchen/[action]")]
public class KitchenServiceController(MongoOrderUpdater orderUpdater) : ControllerBase
{
    /// <summary>
    /// Aceita um pedido específico.
    /// </summary>
    [Authorize(Roles = "Admin, Cozinha")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Aceitar(string id)
    {
        await orderUpdater.UpdateOrderStatusAsync(id, "Aceito");
        return NoContent();
    }

    /// <summary>
    /// Rejeita um pedido com justificativa.
    /// </summary>
    [Authorize(Roles = "Admin, Cozinha")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Rejeitar(string id, [FromBody] string justificativa)
    {
        await orderUpdater.UpdateOrderStatusAsync(id, "Rejeitado", justificativa);
        return NoContent();
    }

    /// <summary>
    /// Cancela um pedido específico.
    /// </summary>
    [Authorize(Roles = "Admin, Cozinha")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Cancelar(string id)
    {
        await orderUpdater.UpdateOrderStatusAsync(id, "Cancelado");
        return NoContent();
    }
    
    /// <summary>
    /// Lista todos os pedidos da base MongoDB.
    /// </summary>
    [Authorize(Roles = "Admin, Cozinha")]
    [HttpGet]
    public async Task<IActionResult> ListarPedidos()
    {
        var pedidos = await orderUpdater.GetAllOrdersAsync();
        return Ok(pedidos);
    }
}