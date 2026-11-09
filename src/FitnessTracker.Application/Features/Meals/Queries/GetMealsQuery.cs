using FitnessTracker.Application.Features.Meals.Responses;
using FitTrackApi.Application.Dto;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Queries;

public record GetMealsQuery(
    string IdentityId,
    int PageNumber = 1,
    int PageSize = 10,
    DateOnly? Date = null,
    DateOnly? From = null,
    DateOnly? To = null)
    : IRequest<PagedListResponse<MealResponse>>;
