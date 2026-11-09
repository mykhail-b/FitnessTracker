using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repository
{
    internal class ExerciseRepository : IExerciseRepository
    {
        private readonly AppDbContext _context;

        public ExerciseRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(WorkoutExercise exercise, CancellationToken cancellationToken = default) =>
            _context.Exercises.AddAsync(exercise, cancellationToken).AsTask();

        public Task<WorkoutExercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            _context.Exercises.SingleOrDefaultAsync(exercise => exercise.Id == id, cancellationToken);

        public Task<WorkoutExercise?> GetReadOnlyByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            _context.Exercises.AsNoTracking().SingleOrDefaultAsync(exercise => exercise.Id == id, cancellationToken);

        public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
            _context.Exercises.CountAsync(cancellationToken);

        public async Task<IReadOnlyList<WorkoutExercise>> GetPageAsync(
            int skip,
            int take,
            CancellationToken cancellationToken = default) =>
            await _context.Exercises
                .AsNoTracking()
                .OrderBy(exercise => exercise.Name)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken) > 0;

        public void Update(WorkoutExercise exercise) => _context.Exercises.Update(exercise);

        public void Remove(WorkoutExercise exercise) => _context.Exercises.Remove(exercise);
    }
}
