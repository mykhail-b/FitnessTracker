using FitnessTracker.Application.Features.Workouts.Commands;
using FitnessTracker.Application.Features.Workouts.Queries;
using FitnessTracker.Application.Features.Workouts.Responses;
using FitTrackApi.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTracker.API.Controllers;

[Authorize]
[Route("api/v1/workout")]
[ApiController]
public class WorkoutController(ISender sender) : ControllerBase
{
    private bool TryGetCurrentIdentityId(out string identityId)
    {
        identityId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        return !string.IsNullOrWhiteSpace(identityId);
    }

    [HttpGet]
    public async Task<ActionResult<PagedListResponse<WorkoutResponse>>> GetWorkouts(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        return Ok(await sender.Send(new GetWorkoutsQuery(identityId, pageNumber, pageSize), ct));
    }

    [HttpGet("activity")]
    public async Task<ActionResult<List<DateOnly>>> GetWorkoutActivity(CancellationToken ct)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        return Ok(await sender.Send(new GetWorkoutActivityQuery(identityId), ct));
    }

    [HttpGet("{workoutId:guid}")]
    public async Task<ActionResult<WorkoutResponse>> GetWorkoutById(Guid workoutId, CancellationToken ct)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        var result = await sender.Send(new GetWorkoutByIdQuery(workoutId, identityId), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<WorkoutResponse>> CreateWorkout(
        [FromBody] CreateWorkoutCommand command,
        CancellationToken ct = default)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        var result = await sender.Send(new CreateWorkoutCommand(identityId, command), ct);
        if (result is null) return NotFound();
        return CreatedAtAction(nameof(GetWorkoutById), new { workoutId = result.Id }, result);
    }

    [HttpPut("{workoutId:guid}")]
    public async Task<IActionResult> UpdateWorkout(
        Guid workoutId,
        [FromBody] UpdateWorkoutCommand command,
        CancellationToken ct = default)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        var updated = await sender.Send(new UpdateWorkoutCommand(workoutId, identityId, command), ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{workoutId:guid}")]
    public async Task<IActionResult> DeleteWorkout(Guid workoutId, CancellationToken ct = default)
    {
        if (!TryGetCurrentIdentityId(out var identityId)) return Unauthorized();
        var deleted = await sender.Send(new DeleteWorkoutCommand(workoutId, identityId), ct);
        return deleted ? NoContent() : NotFound();
    }
}