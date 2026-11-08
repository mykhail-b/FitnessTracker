using System.ComponentModel.DataAnnotations;
using FitTrackApi.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitTrackApi.Server.Controllers;

public record RegisterRequest([Required, EmailAddress] string Email, [Required, MinLength(8)] string Password);
public record LoginRequest([Required, EmailAddress] string Email, [Required] string Password);

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _auth.RegisterAsync(request.Email, request.Password);

        return result.Succeeded
            ? StatusCode(StatusCodes.Status201Created)
            : BadRequest(result.Errors.Select(e => e.Description));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _auth.LoginAsync(request.Email, request.Password);

        return token is null
            ? Unauthorized()
            : Ok(new { accessToken = token });
    }
}