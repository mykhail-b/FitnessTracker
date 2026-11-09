using FitnessTracker.Domain.Entity;

namespace FitnessTracker.Domain.Repository
{
    public interface IWorkoutRepository : IRepository<Workout>
    {
        Task<Workout?> GetByIdWithSetsAsync(Guid workoutId, Guid userId, CancellationToken cancellationToken = default);
        Task<Workout?> GetByIdForUserAsync(Guid workoutId, Guid userId, CancellationToken cancellationToken = default);
        Task<int> CountForUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Workout>> GetForUserPageAsync(Guid userId, int skip, int take, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DateOnly>> GetActivityDatesAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DailyWorkoutStats>> GetActivitySummaryAsync(Guid appUser, DateOnly from, DateOnly to, CancellationToken cancellationToken);
    }

    public record DailyWorkoutStats(DateOnly Date, int Workouts, int Sets, int Reps, decimal Volume);

}
