using FitnessTracker.Application.Features.Exercises.Commands;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class DeleteExerciseHandler : IRequestHandler<DeleteExerciseCommand, bool>
{
    private readonly IExerciseRepository _exerciseRepository;

    public DeleteExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<bool> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId, cancellationToken);
        if (exercise is null) return false;

        _exerciseRepository.Remove(exercise);
        await _exerciseRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
