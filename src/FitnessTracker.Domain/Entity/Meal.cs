namespace FitnessTracker.Domain.Entity;

public class Meal
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = null!;
    public DateOnly Date { get; private set; }
    public List<MealItem> Items { get; private set; } = new();

    private Meal() { }

    public Meal(Guid id, Guid userId, string name, DateOnly date, IEnumerable<MealItem>? items = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid meal ID", nameof(id));
        if (userId == Guid.Empty)
            throw new ArgumentException("Invalid user ID", nameof(userId));

        Id = id;
        UserId = userId;
        UpdateDetails(name, date);
        ReplaceItems(items ?? []);
    }

    public void UpdateDetails(string name, DateOnly date)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length > 100)
            throw new ArgumentException("Meal name cannot exceed 100 characters", nameof(name));

        Name = name;
        Date = date;
    }

    public void ReplaceItems(IEnumerable<MealItem> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        var replacementItems = items.ToList();
        if (replacementItems.Any(item => item.MealId != Id))
            throw new ArgumentException("All items must belong to this meal", nameof(items));

        Items.Clear();
        Items.AddRange(replacementItems);
    }
}
