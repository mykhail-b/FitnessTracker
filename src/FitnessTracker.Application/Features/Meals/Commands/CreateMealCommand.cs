using FitnessTracker.Application.Features.Meals.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Commands;

public record MealItemInput(Guid FoodId, decimal Grams);

public record CreateMealRequest(string Name, DateOnly Date, IReadOnlyList<MealItemInput> Items);

public record CreateMealCommand(string IdentityId, CreateMealRequest Meal)
    : IRequest<MealResponse?>;
