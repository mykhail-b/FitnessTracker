using FitnessTracker.Application.Features.Foods.Responses;
using FitTrackApi.Application.Dto;
using MediatR;

namespace FitnessTracker.Application.Features.Foods.Queries;

public record GetFoodsQuery(int PageNumber = 1, int PageSize = 10, string? Search = null)
    : IRequest<PagedListResponse<FoodResponse>>;
