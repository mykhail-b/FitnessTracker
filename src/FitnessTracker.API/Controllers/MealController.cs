using System.Security.Claims;
using FitnessTracker.Application.Features.Meals.Commands;
using FitnessTracker.Application.Features.Meals.Queries;
using FitnessTracker.Application.Features.Meals.Responses;
using FitTrackApi.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Authorize]
[Route("api/v1/meal")]
[ApiController]
public class MealController(ISender sender) : ControllerBase
{
    private bool TryGetCurrentIdentityId(out string identityId)
    {
        identityId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        return !string.IsNullOrWhiteSpace(identityId);
    }

    [HttpGet]
    public async Task<ActionResult<PagedListResponse<MealResponse>>> GetMeals(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateOnly? date = null,
        [FromQuery] DateOnly? from = null,
        [FromQuery] DateOnly? to = null,
        CancellationToken ct = default)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        return Ok(await sender.Send(new GetMealsQuery(identityId, pageNumber, pageSize, date, from, to), ct));
    }

    [HttpGet("{mealId:guid}")]
    public async Task<ActionResult<MealResponse>> GetMealById(Guid mealId, CancellationToken ct)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        var meal = await sender.Send(new GetMealByIdQuery(mealId, identityId), ct);
        return meal is null ? NotFound() : Ok(meal);
    }

    [HttpPost]
    public async Task<ActionResult<MealResponse>> CreateMeal(
        [FromBody] CreateMealRequest request,
        CancellationToken ct = default)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        var meal = await sender.Send(new CreateMealCommand(identityId, request), ct);
        if (meal is null) return NotFound();
        return CreatedAtAction(nameof(GetMealById), new { mealId = meal.Id }, meal);
    }

    [HttpPut("{mealId:guid}")]
    public async Task<IActionResult> UpdateMeal(
        Guid mealId,
        [FromBody] UpdateMealRequest request,
        CancellationToken ct = default)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        var updated = await sender.Send(new UpdateMealCommand(mealId, identityId, request), ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{mealId:guid}")]
    public async Task<IActionResult> DeleteMeal(Guid mealId, CancellationToken ct = default)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        var deleted = await sender.Send(new DeleteMealCommand(mealId, identityId), ct);
        return deleted ? NoContent() : NotFound();
    }
}
