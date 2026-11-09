using FitnessTracker.Application.Features.Workouts.Responses;
using FitTrackApi.Application.Dto;
using FitnessTracker.Domain.Repository;
using MediatR;
using FitnessTracker.Application.Features.Workouts.Queries;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class GetWorkoutsHandler : IRequestHandler<GetWorkoutsQuery, PagedListResponse<WorkoutResponse>>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public GetWorkoutsHandler(IWorkoutRepository workoutRepository, IUserProfileRepository userProfileRepository)
    {
        _workoutRepository = workoutRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<PagedListResponse<WorkoutResponse>> Handle(
        GetWorkoutsQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(1, request.PageNumber);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null)
        {
            return new PagedListResponse<WorkoutResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        var totalCount = await _workoutRepository.CountForUserAsync(user.Id, cancellationToken);
        var items = await _workoutRepository.GetForUserPageAsync(
            user.Id,
            (pageNumber - 1) * pageSize,
            pageSize,
            cancellationToken);

        return new PagedListResponse<WorkoutResponse>
        {
            Items = items.Select(WorkoutResponse.FromEntity).ToList(),
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
