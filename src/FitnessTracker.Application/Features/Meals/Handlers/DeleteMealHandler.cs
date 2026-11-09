using FitnessTracker.Application.Features.Meals.Commands;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Handlers;

public class DeleteMealHandler : IRequestHandler<DeleteMealCommand, bool>
{
    private readonly IMealRepository _mealRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public DeleteMealHandler(IMealRepository mealRepository, IUserProfileRepository userProfileRepository)
    {
        _mealRepository = mealRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<bool> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return false;

        var meal = await _mealRepository.GetByIdForUserAsync(request.MealId, user.Id, cancellationToken);
        if (meal is null) return false;

        _mealRepository.Remove(meal);
        await _mealRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
