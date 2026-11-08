using FitnessTracker.Application.Features.Exercises.Commands;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class DeleteExerciseHandler : IRequestHandler<DeleteExerciseCommand, bool>
{
    private readonly AppDbContext _db;

    public DeleteExerciseHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _db.Exercises
            .SingleOrDefaultAsync(item => item.Id == request.ExerciseId, cancellationToken);
        if (exercise is null) return false;

        _db.Exercises.Remove(exercise);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
