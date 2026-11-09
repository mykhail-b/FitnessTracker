using MediatR;

namespace FitnessTracker.Application.Features.Meals.Commands;

public record DeleteMealCommand(Guid MealId, string IdentityId) : IRequest<bool>;
