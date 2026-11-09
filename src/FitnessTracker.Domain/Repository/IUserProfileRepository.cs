using FitnessTracker.Domain.Entity;

namespace FitnessTracker.Domain.Repository;

public interface IUserProfileRepository : IRepository<AppUser>
{
    Task<AppUser?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default);
    Task<AppUser?> GetTrackedByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default);
}
