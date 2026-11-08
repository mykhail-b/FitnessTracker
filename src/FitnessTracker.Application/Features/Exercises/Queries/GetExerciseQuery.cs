using FitnessTracker.Application.Features.Exercises.Responses;
using FitTrackApi.Application.Dto;
using MediatR;

namespace FitnessTracker.Application.Features.Exercises.Queries;

public record GetExerciseQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<PagedListResponse<ExerciseResponse>>;
