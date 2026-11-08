using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Commands;

public record DeleteWorkoutCommand(Guid WorkoutId, string IdentityId) : IRequest<bool>;
