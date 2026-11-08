using FitnessTracker.Application.Features.Workouts.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Queries;

public record GetWorkoutByIdQuery(Guid WorkoutId, string IdentityId) : IRequest<WorkoutResponse?>;
