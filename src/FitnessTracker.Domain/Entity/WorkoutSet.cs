namespace FitnessTracker.Domain.Entity;

public class WorkoutSet
{
    public Guid Id { get; private set; }

    public Guid WorkoutId { get; private set; }

    public Guid ExerciseId { get; private set; }

    public int SetNumber { get; private set; }

    public int Reps { get; private set; }

    public decimal Weight { get; private set; }
}