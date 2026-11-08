using FitnessTracker.Application.Features.Workouts.Responses;
using FitTrackApi.Application.Dto;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Queries;

public record GetWorkoutsQuery(string IdentityId, int PageNumber = 1, int PageSize = 10)
    : IRequest<PagedListResponse<WorkoutResponse>>;
