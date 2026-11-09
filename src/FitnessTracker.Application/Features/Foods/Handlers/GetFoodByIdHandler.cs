using FitnessTracker.Application.Features.Foods.Queries;
using FitnessTracker.Application.Features.Foods.Responses;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Foods.Handlers;

public class GetFoodByIdHandler : IRequestHandler<GetFoodByIdQuery, FoodResponse?>
{
    private readonly IFoodRepository _foodRepository;

    public GetFoodByIdHandler(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public async Task<FoodResponse?> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
    {
        var food = await _foodRepository.GetByIdAsync(request.FoodId, cancellationToken);
        return food is null ? null : FoodResponse.FromEntity(food);
    }
}
