using FitnessTracker.Application.Features.Meals.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Commands;

public record UpdateMealRequest(string Name, DateOnly Date, IReadOnlyList<MealItemInput> Items);

public record UpdateMealCommand(Guid MealId, string IdentityId, UpdateMealRequest Meal)
    : IRequest<bool>;
