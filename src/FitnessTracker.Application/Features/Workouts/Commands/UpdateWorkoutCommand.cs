using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Commands;

public record UpdateWorkoutCommand(Guid WorkoutId, string IdentityId, UpdateWorkoutCommand Workout)
    : IRequest<bool>;
