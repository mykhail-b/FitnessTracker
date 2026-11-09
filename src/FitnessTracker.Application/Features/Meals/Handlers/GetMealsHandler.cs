using FitnessTracker.Application.Features.Meals.Queries;
using FitnessTracker.Application.Features.Meals.Responses;
using FitnessTracker.Domain.Repository;
using FitTrackApi.Application.Dto;
using MediatR;

namespace FitnessTracker.Application.Features.Meals.Handlers;

public class GetMealsHandler : IRequestHandler<GetMealsQuery, PagedListResponse<MealResponse>>
{
    private readonly IMealRepository _mealRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public GetMealsHandler(IMealRepository mealRepository, IUserProfileRepository userProfileRepository)
    {
        _mealRepository = mealRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<PagedListResponse<MealResponse>> Handle(
        GetMealsQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(1, request.PageNumber);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);
        var response = new PagedListResponse<MealResponse>
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return response;

        response.TotalCount = await _mealRepository.CountForUserAsync(
            user.Id,
            request.Date,
            request.From,
            request.To,
            cancellationToken);
        var meals = await _mealRepository.GetForUserPageAsync(
            user.Id,
            (pageNumber - 1) * pageSize,
            pageSize,
            request.Date,
            request.From,
            request.To,
            cancellationToken);
        response.Items = meals.Select(MealResponse.FromEntity).ToList();
        return response;
    }
}
