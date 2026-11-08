
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Domain.Entity;

public class WorkoutExercise
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string Equipment { get; private set; } = null!;

    public bool IsWithEquipment { get; private set; }

    public string Category { get; private set; } = null!;

    public MuscleGroup Muscle { get; private set; }

    [MaxLength(200)]
    public string Instruction { get; private set; } = null!;

    private WorkoutExercise() { } // EF Core

    public WorkoutExercise(
        Guid id,
        string name,
        string equipment,
        string category,
        MuscleGroup muscle,
        string instruction)
    {
        Id = id;

        UpdateDetails(
            name,
            equipment,
            category,
            muscle,
            instruction);
    }

    public void UpdateDetails(
        string name,
        string equipment,
        string category,
        MuscleGroup muscle,
        string instruction)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(category);
        ArgumentException.ThrowIfNullOrWhiteSpace(instruction);

        if (instruction.Length > 200)
            throw new ArgumentException("Instruction cannot exceed 200 characters");

        Name = name;
        Equipment = equipment;
        IsWithEquipment = !string.IsNullOrWhiteSpace(equipment);
        Category = category;
        Muscle = muscle;
        Instruction = instruction;
    }
}
