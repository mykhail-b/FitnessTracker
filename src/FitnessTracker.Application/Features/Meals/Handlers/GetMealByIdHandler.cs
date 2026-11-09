using FitnessTracker.Application.Features.Meals.Queries;
using FitnessTracker.Application.Features.Meals.Responses;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Handlers;

public class GetMealByIdHandler : IRequestHandler<GetMealByIdQuery, MealResponse?>
{
    private readonly IMealRepository _mealRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public GetMealByIdHandler(IMealRepository mealRepository, IUserProfileRepository userProfileRepository)
    {
        _mealRepository = mealRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<MealResponse?> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return null;

        var meal = await _mealRepository.GetByIdForUserWithItemsAsync(
            request.MealId,
            user.Id,
            cancellationToken);
        return meal is null ? null : MealResponse.FromEntity(meal);
    }
}
