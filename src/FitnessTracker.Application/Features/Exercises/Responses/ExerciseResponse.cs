namespace FitnessTracker.Application.Features.Exercises.Responses;

public record ExerciseResponse(
    Guid Id,
    string Name,
    string EquipmentName,
    string Category,
    string Muscle,
    string Instruction
);
