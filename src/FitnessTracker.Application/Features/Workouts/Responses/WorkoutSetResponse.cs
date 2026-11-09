namespace FitnessTracker.Application.Features.Workouts.Responses
{
    public record WorkoutSetResponse(
        Guid Id,
        Guid ExerciseId,
        int SetNumber,
        int Reps,
        decimal Weight);
}
