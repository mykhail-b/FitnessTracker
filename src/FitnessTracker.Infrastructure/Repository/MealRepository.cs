using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repository
{
    internal class MealRepository : IMealRepository
    {
        private readonly AppDbContext _context;

        public MealRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Meal meal, CancellationToken cancellationToken = default) =>
            _context.Meals.AddAsync(meal, cancellationToken).AsTask();

        public Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            _context.Meals.SingleOrDefaultAsync(meal => meal.Id == id, cancellationToken);

        public Task<Meal?> GetByIdForUserWithItemsAsync(
            Guid mealId,
            Guid userId,
            CancellationToken cancellationToken = default) =>
            _context.Meals
                .AsNoTracking()
                .Where(meal => meal.Id == mealId && meal.UserId == userId)
                .Include(meal => meal.Items)
                .ThenInclude(item => item.Food)
                .SingleOrDefaultAsync(cancellationToken);

        public Task<Meal?> GetByIdForUserAsync(
            Guid mealId,
            Guid userId,
            CancellationToken cancellationToken = default) =>
            _context.Meals
                .Include(meal => meal.Items)
                .SingleOrDefaultAsync(meal => meal.Id == mealId && meal.UserId == userId, cancellationToken);

        public Task<int> CountForUserAsync(
            Guid userId,
            DateOnly? date,
            DateOnly? from,
            DateOnly? to,
            CancellationToken cancellationToken = default) =>
            ApplyDateFilter(_context.Meals.AsNoTracking().Where(meal => meal.UserId == userId), date, from, to)
                .CountAsync(cancellationToken);

        public async Task<IReadOnlyList<Meal>> GetForUserPageAsync(
            Guid userId,
            int skip,
            int take,
            DateOnly? date,
            DateOnly? from,
            DateOnly? to,
            CancellationToken cancellationToken = default) =>
            await ApplyDateFilter(
                    _context.Meals.AsNoTracking().Where(meal => meal.UserId == userId),
                    date,
                    from,
                    to)
                .OrderByDescending(meal => meal.Date)
                .ThenBy(meal => meal.Name)
                .Skip(skip)
                .Take(take)
                .Include(meal => meal.Items)
                .ThenInclude(item => item.Food)
                .ToListAsync(cancellationToken);

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken) > 0;

        public void Update(Meal meal) => _context.Meals.Update(meal);

        public void Remove(Meal meal) => _context.Meals.Remove(meal);

        private static IQueryable<Meal> ApplyDateFilter(
            IQueryable<Meal> meals,
            DateOnly? date,
            DateOnly? from,
            DateOnly? to)
        {
            if (date.HasValue)
                meals = meals.Where(meal => meal.Date == date.Value);
            if (from.HasValue)
                meals = meals.Where(meal => meal.Date >= from.Value);
            if (to.HasValue)
                meals = meals.Where(meal => meal.Date <= to.Value);

            return meals;
        }
    }
}
