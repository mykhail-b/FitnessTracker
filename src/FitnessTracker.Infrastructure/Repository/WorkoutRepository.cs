using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repository
{
    internal class WorkoutRepository : IWorkoutRepository
    {
        private readonly AppDbContext _context;

        public WorkoutRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Workout entity, CancellationToken cancellationToken = default) =>
            _context.Workouts.AddAsync(entity, cancellationToken).AsTask();

        public Task<Workout?> GetByIdWithSetsAsync(Guid workoutId, Guid userId, CancellationToken cancellationToken = default) =>
            _context.Workouts
                .AsNoTracking()
                .Include(workout => workout.WorkoutSets)
                .SingleOrDefaultAsync(
                    workout => workout.Id == workoutId && workout.UserId == userId,
                    cancellationToken);

        public Task<Workout?> GetByIdForUserAsync(Guid workoutId, Guid userId, CancellationToken cancellationToken = default) =>
            _context.Workouts
                .SingleOrDefaultAsync(
                    workout => workout.Id == workoutId && workout.UserId == userId,
                    cancellationToken);

        public Task<Workout?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            _context.Workouts.SingleOrDefaultAsync(workout => workout.Id == id, cancellationToken);

        public Task<int> CountForUserAsync(Guid userId, CancellationToken cancellationToken = default) =>
            _context.Workouts.CountAsync(workout => workout.UserId == userId, cancellationToken);

        public async Task<IReadOnlyList<Workout>> GetForUserPageAsync(Guid userId, int skip, int take, CancellationToken cancellationToken = default) =>
            await _context.Workouts
                .AsNoTracking()
                .Where(workout => workout.UserId == userId)
                .OrderByDescending(workout => workout.Date)
                .Skip(skip)
                .Take(take)
                .Include(workout => workout.WorkoutSets)
                .ToListAsync(cancellationToken);

        public async Task<IReadOnlyList<DailyWorkoutStats>> GetActivitySummaryAsync(
            Guid appUser,
            DateOnly from,
            DateOnly to,
            CancellationToken cancellationToken)
        {
            var workouts = await _context.Workouts
                .AsNoTracking()
                .Where(workout =>
                    workout.UserId == appUser &&
                    DateOnly.FromDateTime(workout.Date) >= from &&
                    DateOnly.FromDateTime(workout.Date) <= to)
                .Include(workout => workout.WorkoutSets)
                .ToListAsync(cancellationToken);

            return workouts
                .GroupBy(workout => DateOnly.FromDateTime(workout.Date))
                .Select(group => new DailyWorkoutStats(
                    group.Key,
                    group.Count(),
                    group.Sum(workout => workout.WorkoutSets.Count),
                    group.Sum(workout => workout.WorkoutSets.Sum(set => set.Reps)),
                    group.Sum(workout => workout.WorkoutSets.Sum(set => set.Reps * set.Weight))))
                .OrderBy(day => day.Date)
                .ToList();
        }

        public async Task<IReadOnlyList<DateOnly>> GetActivityDatesAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var dates = await _context.Workouts
                .AsNoTracking()
                .Where(workout => workout.UserId == userId)
                .Select(workout => workout.Date)
                .ToListAsync(cancellationToken);

            return dates
                .Select(DateOnly.FromDateTime)
                .Distinct()
                .Order()
                .ToList();
        }

        public void Remove(Workout entity) => _context.Workouts.Remove(entity);

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken) > 0;

        public void Update(Workout entity) => _context.Workouts.Update(entity);
    }
}
