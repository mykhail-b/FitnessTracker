using FitnessTracker.Application.Features.Exercises.Responses;
using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using MediatR;
using FitnessTracker.Application.Features.Exercises.Commands;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, ExerciseResponse>
{
    private readonly IExerciseRepository _exerciseRepository;

    public CreateExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
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

        await _exerciseRepository.AddAsync(exercise, cancellationToken);
        await _exerciseRepository.SaveChangesAsync(cancellationToken);

        return new ExerciseResponse(
            exercise.Id,
            exercise.Name,
            exercise.Equipment,
            exercise.Category,
            exercise.Muscle.ToString(),
            exercise.Instruction);
    }
}
