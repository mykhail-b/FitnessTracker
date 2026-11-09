using FitnessTracker.Domain.Entity;

namespace FitnessTracker.Domain.Repository
{
    public interface IMealRepository : IRepository<Meal>
    {
        Task<Meal?> GetByIdForUserWithItemsAsync(Guid mealId, Guid userId, CancellationToken cancellationToken = default);
        Task<Meal?> GetByIdForUserAsync(Guid mealId, Guid userId, CancellationToken cancellationToken = default);
        Task<int> CountForUserAsync(
            Guid userId,
            DateOnly? date,
            DateOnly? from,
            DateOnly? to,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Meal>> GetForUserPageAsync(
            Guid userId,
            int skip,
            int take,
            DateOnly? date,
            DateOnly? from,
            DateOnly? to,
            CancellationToken cancellationToken = default);
    }
}
