using FitnessTracker.Application.Features.Workouts.Commands;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class DeleteWorkoutHandler : IRequestHandler<DeleteWorkoutCommand, bool>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public DeleteWorkoutHandler(IWorkoutRepository workoutRepository, IUserProfileRepository userProfileRepository)
    {
        _workoutRepository = workoutRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<bool> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return false;

        var workout = await _workoutRepository.GetByIdForUserAsync(
            request.WorkoutId,
            user.Id,
            cancellationToken);
        if (workout is null) return false;

        _workoutRepository.Remove(workout);
        await _workoutRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
