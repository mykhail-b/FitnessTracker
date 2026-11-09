using FitnessTracker.Application.Features.Meals.Commands;
using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Handlers;

public class UpdateMealHandler : IRequestHandler<UpdateMealCommand, bool>
{
    private readonly IMealRepository _mealRepository;
    private readonly IFoodRepository _foodRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public UpdateMealHandler(
        IMealRepository mealRepository,
        IFoodRepository foodRepository,
        IUserProfileRepository userProfileRepository)
    {
        _mealRepository = mealRepository;
        _foodRepository = foodRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<bool> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return false;

        var meal = await _mealRepository.GetByIdForUserAsync(request.MealId, user.Id, cancellationToken);
        if (meal is null) return false;

        var distinctFoodIds = request.Meal.Items.Select(item => item.FoodId).Distinct().ToArray();
        var foods = await _foodRepository.GetByIdsAsync(distinctFoodIds, cancellationToken);
        var foodsById = foods.ToDictionary(food => food.Id);
        if (foodsById.Count != distinctFoodIds.Length) return false;

        meal.UpdateDetails(request.Meal.Name, request.Meal.Date);
        meal.ReplaceItems(request.Meal.Items
            .Select(item => new MealItem(Guid.NewGuid(), meal.Id, foodsById[item.FoodId], item.Grams)));
        await _mealRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
