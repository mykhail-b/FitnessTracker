using FitnessTracker.Application.Features.Foods.Queries;
using FitnessTracker.Application.Features.Foods.Responses;
using FitnessTracker.Domain.Repository;
using FitTrackApi.Application.Dto;
using MediatR;

namespace FitnessTracker.Application.Features.Foods.Handlers;

public class GetFoodsHandler : IRequestHandler<GetFoodsQuery, PagedListResponse<FoodResponse>>
{
    private readonly IFoodRepository _foodRepository;

    public GetFoodsHandler(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public async Task<PagedListResponse<FoodResponse>> Handle(
        GetFoodsQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(1, request.PageNumber);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);
        var totalCount = await _foodRepository.CountAsync(request.Search, cancellationToken);
        var foods = await _foodRepository.GetPageAsync(
            (pageNumber - 1) * pageSize,
            pageSize,
            request.Search,
            cancellationToken);

        return new PagedListResponse<FoodResponse>
        {
            Items = foods.Select(FoodResponse.FromEntity).ToList(),
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
