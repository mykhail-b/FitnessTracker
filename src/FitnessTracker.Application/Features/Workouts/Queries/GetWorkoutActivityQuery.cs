using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Queries;

public record GetWorkoutActivityQuery(string IdentityId) : IRequest<List<DateOnly>>;
