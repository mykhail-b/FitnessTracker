
namespace FitnessTracker.Domain.Entity;

public class Workout
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime Date { get; private set; } = DateTime.UtcNow;

    public string? Notes { get; private set; }

    public List<WorkoutSet> WorkoutSets { get; private set; } = new();

    private Workout() { }

    public Workout(Guid id, Guid userId, string? notes = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Invalid user ID");

        Id = id;
        UserId = userId;
        Notes = notes;
    }

    public void UpdateNotes(string? notes)
    {
        Notes = notes;
    }
}
