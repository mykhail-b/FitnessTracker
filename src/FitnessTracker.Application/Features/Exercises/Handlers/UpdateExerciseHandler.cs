using FitnessTracker.Application.Features.Exercises.Commands;
using FitnessTracker.Application.Features.Exercises.Responses;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class UpdateExerciseHandler : IRequestHandler<UpdateExerciseCommand, ExerciseResponse?>
{
    private readonly IExerciseRepository _exerciseRepository;

    public UpdateExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<ExerciseResponse?> Handle(
        UpdateExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId, cancellationToken);
        if (exercise is null) return null;

        exercise.UpdateDetails(
            request.Name,
            request.Equipment,
            request.Category,
            request.Muscle,
            request.Instruction);
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
