
using FitnessTracker.Application.Features.Exercises.Queries;
using FitnessTracker.Application.Features.Exercises.Responses;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class GetExerciseByIdHandler : IRequestHandler<GetExerciseByIdQuery, ExerciseResponse?>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseByIdHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<ExerciseResponse?> Handle(
        GetExerciseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetReadOnlyByIdAsync(request.ExerciseId, cancellationToken);

        return exercise is null
            ? null
            : new ExerciseResponse(
                exercise.Id,
                exercise.Name,
                exercise.Equipment,
                exercise.Category,
                exercise.Muscle.ToString(),
                exercise.Instruction);
    }
}
