using FitnessTracker.Application.Features.Meals.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Queries;

public record GetMealByIdQuery(Guid MealId, string IdentityId) : IRequest<MealResponse?>;
