using FitnessTracker.Application.Features.Workouts.Queries;
using FitnessTracker.Application.Features.Workouts.Responses;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class GetActivitySummaryHandler : IRequestHandler<GetActivitySummaryQuery, ActivitySummaryResponse>
{
    private readonly IWorkoutRepository _repository;

    public GetActivitySummaryHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }

    public async Task<ActivitySummaryResponse> Handle(GetActivitySummaryQuery r, CancellationToken ct)
    {
        if (r.From > r.To)
            throw new ArgumentException("From must be earlier than To");

        if (r.To.DayNumber - r.From.DayNumber > 366)
            throw new ArgumentException("Period cannot exceed one year");

        var days = await _repository.GetActivitySummaryAsync(r.AccountId, r.From, r.To, ct);

        return new ActivitySummaryResponse(
            r.From,
            r.To,
            WorkoutsCount: days.Sum(d => d.Workouts),
            TotalSets: days.Sum(d => d.Sets),
            TotalReps: days.Sum(d => d.Reps),
            TotalVolume: days.Sum(d => d.Volume),
            Days: days.Select(d => new DaySummary(d.Date, d.Sets, d.Reps, d.Volume)).ToList());
    }
}
