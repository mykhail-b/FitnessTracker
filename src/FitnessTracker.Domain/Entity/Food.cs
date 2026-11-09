namespace FitnessTracker.Domain.Entity;

public class Food
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal CaloriesPer100g { get; private set; }
    public decimal ProteinPer100g { get; private set; }
    public decimal FatPer100g { get; private set; }
    public decimal CarbsPer100g { get; private set; }

    private Food() { }

    public Food(
        Guid id,
        string name,
        decimal caloriesPer100g,
        decimal proteinPer100g,
        decimal fatPer100g,
        decimal carbsPer100g)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid food ID", nameof(id));
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length > 100)
            throw new ArgumentException("Food name cannot exceed 100 characters", nameof(name));
        if (caloriesPer100g < 0)
            throw new ArgumentOutOfRangeException(nameof(caloriesPer100g));
        if (proteinPer100g < 0)
            throw new ArgumentOutOfRangeException(nameof(proteinPer100g));
        if (fatPer100g < 0)
            throw new ArgumentOutOfRangeException(nameof(fatPer100g));
        if (carbsPer100g < 0)
            throw new ArgumentOutOfRangeException(nameof(carbsPer100g));

        Id = id;
        Name = name;
        CaloriesPer100g = caloriesPer100g;
        ProteinPer100g = proteinPer100g;
        FatPer100g = fatPer100g;
        CarbsPer100g = carbsPer100g;
    }
}
