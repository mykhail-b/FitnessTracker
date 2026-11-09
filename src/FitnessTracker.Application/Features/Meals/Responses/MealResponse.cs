using FitnessTracker.Domain.Entity;
using FitnessTracker.Application.Features.Foods.Responses;

namespace FitnessTracker.Application.Features.Meals.Responses;

public record MealItemResponse(
    Guid Id,
    FoodResponse Food,
    decimal Grams,
    decimal Calories,
    decimal Protein,
    decimal Fat,
    decimal Carbs);

public record MealResponse(
    Guid Id,
    string Name,
    DateOnly Date,
    IReadOnlyList<MealItemResponse> Items,
    decimal TotalCalories,
    decimal TotalProtein,
    decimal TotalFat,
    decimal TotalCarbs)
{
    public static MealResponse FromEntity(Meal meal)
    {
        var items = meal.Items
            .Select(item => new MealItemResponse(
                item.Id,
                FoodResponse.FromEntity(item.Food),
                item.Grams,
                item.Grams * item.Food.CaloriesPer100g / 100,
                item.Grams * item.Food.ProteinPer100g / 100,
                item.Grams * item.Food.FatPer100g / 100,
                item.Grams * item.Food.CarbsPer100g / 100))
            .ToList();

        return new MealResponse(
            meal.Id,
            meal.Name,
            meal.Date,
            items,
            items.Sum(item => item.Calories),
            items.Sum(item => item.Protein),
            items.Sum(item => item.Fat),
            items.Sum(item => item.Carbs));
    }
}
