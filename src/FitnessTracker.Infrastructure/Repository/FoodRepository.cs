using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repository;

internal class FoodRepository : IFoodRepository
{
    private readonly AppDbContext _context;

    public FoodRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Food?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _context.Foods.AsNoTracking().SingleOrDefaultAsync(food => food.Id == id, cancellationToken);

    public Task<int> CountAsync(string? search, CancellationToken cancellationToken = default) =>
        ApplySearch(_context.Foods.AsNoTracking(), search).CountAsync(cancellationToken);

    public async Task<IReadOnlyList<Food>> GetByIdsAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken = default) =>
        await _context.Foods
            .AsNoTracking()
            .Where(food => ids.Contains(food.Id))
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Food>> GetPageAsync(
        int skip,
        int take,
        string? search,
        CancellationToken cancellationToken = default) =>
        await ApplySearch(_context.Foods.AsNoTracking(), search)
            .OrderBy(food => food.Name)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

    public Task AddAsync(Food entity, CancellationToken cancellationToken = default) =>
        _context.Foods.AddAsync(entity, cancellationToken).AsTask();

    public void Update(Food entity) => _context.Foods.Update(entity);

    public void Remove(Food entity) => _context.Foods.Remove(entity);

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken) > 0;

    private static IQueryable<Food> ApplySearch(IQueryable<Food> foods, string? search) =>
        string.IsNullOrWhiteSpace(search)
            ? foods
            : foods.Where(food => food.Name.Contains(search));
}
