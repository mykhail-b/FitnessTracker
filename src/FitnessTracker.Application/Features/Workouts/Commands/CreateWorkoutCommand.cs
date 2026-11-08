using FitnessTracker.Application.Features.Workouts.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Commands;

public record CreateWorkoutCommand(string IdentityId, CreateWorkoutCommand Workout)
    : IRequest<WorkoutResponse?>;
