using FitnessTracker.Application.Features.Foods.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.Foods.Queries;

public record GetFoodByIdQuery(Guid FoodId) : IRequest<FoodResponse?>;
