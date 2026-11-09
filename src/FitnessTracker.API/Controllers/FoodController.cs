using FitnessTracker.Application.Features.Foods.Queries;
using FitnessTracker.Application.Features.Foods.Responses;
using FitTrackApi.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Authorize]
[Route("api/v1/food")]
[ApiController]
public class FoodController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<FoodResponse>>> GetFoods(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        CancellationToken ct = default)
    {
        return Ok(await sender.Send(new GetFoodsQuery(pageNumber, pageSize, search), ct));
    }

    [HttpGet("{foodId:guid}")]
    public async Task<ActionResult<FoodResponse>> GetFoodById(Guid foodId, CancellationToken ct)
    {
        var food = await sender.Send(new GetFoodByIdQuery(foodId), ct);
        return food is null ? NotFound() : Ok(food);
    }
}
