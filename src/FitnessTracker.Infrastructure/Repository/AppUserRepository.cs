using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repository
{
    internal class AppUserRepository : IUserProfileRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<AppUser?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default) =>
            _context.UserProfiles
                .AsNoTracking()
                .SingleOrDefaultAsync(profile => profile.IdentityId == identityId, cancellationToken);

        public Task<AppUser?> GetTrackedByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default) =>
            _context.UserProfiles
                .SingleOrDefaultAsync(profile => profile.IdentityId == identityId, cancellationToken);

        public Task<AppUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            _context.UserProfiles
                .SingleOrDefaultAsync(profile => profile.Id == id, cancellationToken);

        public Task AddAsync(AppUser entity, CancellationToken cancellationToken = default) =>
            _context.UserProfiles.AddAsync(entity, cancellationToken).AsTask();

        public void Update(AppUser entity) => _context.UserProfiles.Update(entity);

        public void Remove(AppUser entity) => _context.UserProfiles.Remove(entity);

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
