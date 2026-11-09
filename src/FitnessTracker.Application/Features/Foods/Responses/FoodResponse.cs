using FitnessTracker.Domain.Entity;

namespace FitnessTracker.Application.Features.Foods.Responses;

public record FoodResponse(
    Guid Id,
    string Name,
    decimal CaloriesPer100g,
    decimal ProteinPer100g,
    decimal FatPer100g,
    decimal CarbsPer100g)
{
    public static FoodResponse FromEntity(Food food) =>
        new(
            food.Id,
            food.Name,
            food.CaloriesPer100g,
            food.ProteinPer100g,
            food.FatPer100g,
            food.CarbsPer100g);
}
