using KitchenService.Application.Contracts;
using KitchenService.Domain.Entities;
using KitchenService.Domain.Interfaces;
using MassTransit;
using OrderService.Application.Contracts;

namespace KitchenService.Application.Handlers;

public class KitchenServiceHandler : IConsumer<OrderCreatedEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IKitchenDecisionRepository _repository;
    private const string OrderStatusChangedEvent = "order-status-changed";

    public KitchenServiceHandler(ISendEndpointProvider sendEndpointProvider, IKitchenDecisionRepository repository)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var order = context.Message;

        var decision = new KitchenDecision
        {
            OrderId = order.Id,
            Decision = "Aceito",
            Justification = null
        };

        await _repository.AddAsync(decision);
        
        var eventMessage = new OrderStatusChangedEvent
        {
            Id = decision.OrderId,
            NewStatus = decision.Decision,
            Justification = decision.Justification
        };

        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{OrderStatusChangedEvent}"));
        await endpoint.Send(eventMessage);

        Console.WriteLine($"[KITCHEN] Pedido {order.Id} {decision.Decision}.");
    }
}