namespace FitnessTracker.Domain.Entity;

public class MealItem
{
    public Guid Id { get; private set; }
    public Guid MealId { get; private set; }
    public Guid FoodId { get; private set; }
    public Food Food { get; private set; } = null!;
    public decimal Grams { get; private set; }

    private MealItem() { }

    public MealItem(Guid id, Guid mealId, Food food, decimal grams)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid meal item ID", nameof(id));
        if (mealId == Guid.Empty)
            throw new ArgumentException("Invalid meal ID", nameof(mealId));
        ArgumentNullException.ThrowIfNull(food);
        if (grams <= 0)
            throw new ArgumentOutOfRangeException(nameof(grams), "Grams must be greater than zero");

        Id = id;
        MealId = mealId;
        FoodId = food.Id;
        Food = food;
        Grams = grams;
    }
}
