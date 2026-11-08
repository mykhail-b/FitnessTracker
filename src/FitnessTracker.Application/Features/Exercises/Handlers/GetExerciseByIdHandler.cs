
using FitnessTracker.Application.Features.Exercises.Queries;
using FitnessTracker.Application.Features.Exercises.Responses;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class GetExerciseByIdHandler : IRequestHandler<GetExerciseByIdQuery, ExerciseResponse?>
{
    private readonly AppDbContext _db;

    public GetExerciseByIdHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ExerciseResponse?> Handle(
        GetExerciseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var exercise = await _db.Exercises
            .AsNoTracking()
            .SingleOrDefaultAsync(item => item.Id == request.ExerciseId, cancellationToken);

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
