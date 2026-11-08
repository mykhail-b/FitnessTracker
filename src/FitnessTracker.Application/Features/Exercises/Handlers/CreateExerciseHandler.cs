using FitnessTracker.Application.Features.Exercises.Responses;
using FitnessTracker.Infrastructure.Data;
using FitnessTracker.Domain.Entity;
using MediatR;
using FitnessTracker.Application.Features.Exercises.Commands;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, ExerciseResponse>
{
    private readonly AppDbContext _db;

    public CreateExerciseHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ExerciseResponse> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = new WorkoutExercise(
            Guid.NewGuid(),
            request.Name,
            request.Equipment,
            request.Category,
            request.Muscle,
            request.Instruction);

        _db.Exercises.Add(exercise);
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
