namespace FitnessTracker.Application.Features.Workouts.Responses
{
    public record ActivitySummaryResponse(
        DateOnly From,
        DateOnly To,
        int WorkoutsCount,
        int TotalSets,
        int TotalReps,
        decimal TotalVolume,
        List<DaySummary> Days);

    public record DaySummary(DateOnly Date, int Sets, int Reps, decimal Volume);
}
