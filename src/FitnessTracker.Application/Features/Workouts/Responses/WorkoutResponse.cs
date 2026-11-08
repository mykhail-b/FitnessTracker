using FitnessTracker.Domain.Entity;

namespace FitnessTracker.Application.Features.Workouts.Responses;

public record WorkoutResponse(
    Guid Id,
    DateTime Date,
    string? Notes,
    IReadOnlyList<WorkoutSetResponse> Sets)
{
    public static WorkoutResponse FromEntity(Workout workout) =>
        new(
            workout.Id,
            workout.Date,
            workout.Notes,
            workout.WorkoutSets
                .Select(set => new WorkoutSetResponse(set.Id, set.ExerciseId, set.SetNumber, set.Reps, set.Weight))
                .ToList());
}

public record WorkoutSetResponse(
    Guid Id,
    Guid ExerciseId,
    int SetNumber,
    int Reps,
    decimal Weight);
