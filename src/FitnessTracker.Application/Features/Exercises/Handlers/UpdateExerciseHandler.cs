using FitnessTracker.Application.Features.Exercises.Commands;
using FitnessTracker.Application.Features.Exercises.Responses;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class UpdateExerciseHandler : IRequestHandler<UpdateExerciseCommand, ExerciseResponse?>
{
    private readonly AppDbContext _db;

    public UpdateExerciseHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ExerciseResponse?> Handle(
        UpdateExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var exercise = await _db.Exercises
            .SingleOrDefaultAsync(item => item.Id == request.ExerciseId, cancellationToken);
        if (exercise is null) return null;

        exercise.UpdateDetails(
            request.Name,
            request.Equipment,
            request.Category,
            request.Muscle,
            request.Instruction);
        await _db.SaveChangesAsync(cancellationToken);

        return new ExerciseResponse(
            exercise.Id,
            exercise.Name,
            exercise.Equipment,
            exercise.Category,
            exercise.Muscle.ToString(),
            exercise.Instruction);
    }
}
