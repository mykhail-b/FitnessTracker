using System.Security.Claims;
using FitnessTracker.Application.Features.AppUser.Commands;
using FitnessTracker.Application.Features.AppUser.Queries;
using FitnessTracker.Application.Features.AppUser.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Authorize]
[Route("api/v1/user")]
[ApiController]
public class UserController(ISender sender) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<UserProfileResponse>> GetMyInfo(CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await sender.Send(new GetUserQuery(userId), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserProfileResponse>> GetUserInfo(string userId, CancellationToken ct)
    {
        var result = await sender.Send(new GetUserQuery(userId), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("{userId}")]
    public async Task<ActionResult<UserProfileResponse>> UpdateUserInfo(
        string userId,
        [FromBody] UpdateUserCommand request,
        CancellationToken ct)
    {
        var result = await sender.Send(new UpdateUserCommand(userId, request.FirstName, request.LastName, request.BirthDate), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(string userId, CancellationToken ct)
    {
        var deleted = await sender.Send(new DeleteUserCommand(userId), ct);
        return deleted ? NoContent() : NotFound();
    }
}