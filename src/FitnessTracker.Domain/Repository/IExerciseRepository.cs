using FitnessTracker.Domain.Entity;

namespace FitnessTracker.Domain.Repository
{
    public interface IExerciseRepository : IRepository<WorkoutExercise>
    {
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<WorkoutExercise>> GetPageAsync(int skip, int take, CancellationToken cancellationToken = default);
        Task<WorkoutExercise?> GetReadOnlyByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
