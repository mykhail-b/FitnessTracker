
namespace FitnessTracker.Domain.Entity;

public class Meal
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime Date { get; private set; }

    public double TotalCalories { get; private set; }

    public double TotalProtein { get; private set; }

    public double TotalFat { get; private set; }

    public double TotalCarbs { get; private set; }

    private Meal() { }

    public Meal(Guid id, Guid userId, DateTime date)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Invalid user ID");

        Id = id;
        UserId = userId;
        Date = date;
    }
}
