using FitnessTracker.Application.Features.Exercises.Responses;
using FitTrackApi.Application.Dto;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Application.Features.Exercises.Queries;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class GetExercisesHandler : IRequestHandler<GetExerciseQuery, PagedListResponse<ExerciseResponse>>
{
    private readonly AppDbContext _db;

    public GetExercisesHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedListResponse<ExerciseResponse>> Handle(
        GetExerciseQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(1, request.PageNumber);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);
        var exercises = _db.Exercises.AsNoTracking();
        var totalCount = await exercises.CountAsync(cancellationToken);
        var items = await exercises
            .OrderBy(exercise => exercise.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(exercise => new ExerciseResponse(
                exercise.Id,
                exercise.Name,
                exercise.Equipment,
                exercise.Category,
                exercise.Muscle.ToString(),
                exercise.Instruction))
            .ToListAsync(cancellationToken);

        return new PagedListResponse<ExerciseResponse>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
