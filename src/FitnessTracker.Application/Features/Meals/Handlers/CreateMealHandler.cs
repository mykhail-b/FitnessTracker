using FitnessTracker.Application.Features.Meals.Commands;
using FitnessTracker.Application.Features.Meals.Responses;
using FitnessTracker.Domain.Entity;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Handlers;

public class CreateMealHandler : IRequestHandler<CreateMealCommand, MealResponse?>
{
    private readonly IMealRepository _mealRepository;
    private readonly IFoodRepository _foodRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public CreateMealHandler(
        IMealRepository mealRepository,
        IFoodRepository foodRepository,
        IUserProfileRepository userProfileRepository)
    {
        _mealRepository = mealRepository;
        _foodRepository = foodRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<MealResponse?> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return null;

        var distinctFoodIds = request.Meal.Items.Select(item => item.FoodId).Distinct().ToArray();
        var foods = await _foodRepository.GetByIdsAsync(distinctFoodIds, cancellationToken);
        var foodsById = foods.ToDictionary(food => food.Id);
        if (foodsById.Count != distinctFoodIds.Length) return null;

        var mealId = Guid.NewGuid();
        var items = request.Meal.Items
            .Select(item => new MealItem(Guid.NewGuid(), mealId, foodsById[item.FoodId], item.Grams))
            .ToList();
        var meal = new Meal(mealId, user.Id, request.Meal.Name, request.Meal.Date, items);

        await _mealRepository.AddAsync(meal, cancellationToken);
        await _mealRepository.SaveChangesAsync(cancellationToken);
        return MealResponse.FromEntity(meal);
    }
}
