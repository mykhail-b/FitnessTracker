using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
