using FitnessTracker.Domain.Entity;

namespace FitnessTracker.Domain.Repository;

public interface IFoodRepository : IRepository<Food>
{
    Task<int> CountAsync(string? search, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Food>> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Food>> GetPageAsync(
        int skip,
        int take,
        string? search,
        CancellationToken cancellationToken = default);
}
