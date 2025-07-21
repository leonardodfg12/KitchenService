using KitchenService.Domain.Entities;

namespace KitchenService.Domain.Interfaces;

public interface IKitchenDecisionRepository
{
    Task AddAsync(KitchenDecision decision);
}