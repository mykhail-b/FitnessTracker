using FitnessTracker.Application.Features.Exercises.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.Exercises.Queries;

public record GetExerciseByIdQuery(Guid ExerciseId) : IRequest<ExerciseResponse?>;
