using FitnessTracker.Application.Features.Exercises.Responses;
using FitTrackApi.Application.Dto;
using FitnessTracker.Domain.Repository;
using MediatR;
using FitnessTracker.Application.Features.Exercises.Queries;

namespace FitnessTracker.Application.Features.Exercises.Handlers;

public class GetExercisesHandler : IRequestHandler<GetExerciseQuery, PagedListResponse<ExerciseResponse>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExercisesHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<PagedListResponse<ExerciseResponse>> Handle(
        GetExerciseQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(1, request.PageNumber);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);
        var totalCount = await _exerciseRepository.CountAsync(cancellationToken);
        var exercises = await _exerciseRepository.GetPageAsync(
            (pageNumber - 1) * pageSize,
            pageSize,
            cancellationToken);
        var items = exercises.Select(exercise => new ExerciseResponse(
            exercise.Id,
            exercise.Name,
            exercise.Equipment,
            exercise.Category,
            exercise.Muscle.ToString(),
            exercise.Instruction)).ToList();

        return new PagedListResponse<ExerciseResponse>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
