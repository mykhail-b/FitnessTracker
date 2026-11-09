using FitnessTracker.Application.Features.Workouts.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Queries
{
    public record GetActivitySummaryQuery(Guid AccountId, DateOnly From, DateOnly To) : IRequest<ActivitySummaryResponse>;
}
