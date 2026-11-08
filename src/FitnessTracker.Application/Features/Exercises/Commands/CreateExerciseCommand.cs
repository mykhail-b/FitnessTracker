using FitnessTracker.Application.Features.Exercises.Responses;
using FitnessTracker.Domain.Entity;
using MediatR;

namespace FitnessTracker.Application.Features.Exercises.Commands;

public record CreateExerciseCommand(
    string Name,
    string Equipment,
    string Category,
    MuscleGroup Muscle,
    string Instruction
) : IRequest<ExerciseResponse>;
