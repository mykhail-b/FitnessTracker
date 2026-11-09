using FitnessTracker.Application.Features.Workouts.Commands;
using FitnessTracker.Application.Features.Workouts.Responses;
using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class CreateWorkoutHandler : IRequestHandler<CreateWorkoutCommand, WorkoutResponse?>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public CreateWorkoutHandler(IWorkoutRepository workoutRepository, IUserProfileRepository userProfileRepository)
    {
        _workoutRepository = workoutRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<WorkoutResponse?> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return null;

        var workout = new Workout(Guid.NewGuid(), user.Id);
        await _workoutRepository.AddAsync(workout, cancellationToken);
        await _workoutRepository.SaveChangesAsync(cancellationToken);
        return WorkoutResponse.FromEntity(workout);
    }
}
