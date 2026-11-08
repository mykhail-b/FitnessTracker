using MediatR;

namespace FitnessTracker.Application.Features.Exercises.Commands;

public record DeleteExerciseCommand(Guid ExerciseId) : IRequest<bool>;
