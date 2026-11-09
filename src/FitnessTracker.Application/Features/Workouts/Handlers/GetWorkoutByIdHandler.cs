using FitnessTracker.Application.Features.Workouts.Queries;
using FitnessTracker.Application.Features.Workouts.Responses;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class GetWorkoutByIdHandler : IRequestHandler<GetWorkoutByIdQuery, WorkoutResponse?>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public GetWorkoutByIdHandler(
        IWorkoutRepository workoutRepository,
        IUserProfileRepository userProfileRepository)
    {
        _workoutRepository = workoutRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<WorkoutResponse?> Handle(GetWorkoutByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return null;

        var workout = await _workoutRepository.GetByIdWithSetsAsync(
            request.WorkoutId,
            user.Id,
            cancellationToken);

        return workout is null ? null : WorkoutResponse.FromEntity(workout);
    }
}
