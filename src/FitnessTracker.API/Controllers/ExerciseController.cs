using FitnessTracker.Application.Features.Exercises.Commands;
using FitnessTracker.Application.Features.Exercises.Queries;
using FitnessTracker.Application.Features.Exercises.Responses;
using FitTrackApi.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public class ExerciseController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ExerciseResponse>>> GetAllExercises(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        => Ok(await sender.Send(new GetExerciseQuery(pageNumber, pageSize), ct));

    [HttpGet("{exerciseId:guid}")]
    public async Task<ActionResult<ExerciseResponse>> GetExerciseById(
        [FromRoute] Guid exerciseId,
        CancellationToken ct = default)
    {
        var result = await sender.Send(new GetExerciseByIdQuery(exerciseId), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseResponse>> CreateExercise(
        [FromBody] CreateExerciseCommand request,
        CancellationToken ct = default)
    {
        var result = await sender.Send(request, ct);
        return CreatedAtAction(nameof(GetExerciseById), new { exerciseId = result.Id }, result);
    }

    [HttpPut("{exerciseId:guid}")]
    public async Task<ActionResult<ExerciseResponse>> UpdateExercise(
        [FromRoute] Guid exerciseId,
        [FromBody] UpdateExerciseCommand request,
        CancellationToken ct = default)
    {
        var result = await sender.Send(request with { ExerciseId = exerciseId }, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{exerciseId:guid}")]
    public async Task<ActionResult<ExerciseResponse>> DeleteExercise(
        [FromRoute] Guid exerciseId,
        CancellationToken ct = default)
        => await sender.Send(new DeleteExerciseCommand(exerciseId), ct)
            ? NoContent()
            : NotFound();
}