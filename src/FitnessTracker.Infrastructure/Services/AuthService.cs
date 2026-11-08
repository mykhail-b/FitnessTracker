using System.Security.Claims;
using System.Text;
using FitTrackApi.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace FitnessTracker.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _config;

    public AuthService(UserManager<IdentityUser> userManager, IEmailService emailService, IConfiguration config)
    {
        _userManager = userManager;
        _emailService = emailService;
        _config = config;
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        var user = new IdentityUser { UserName = email, Email = email };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _emailService.SendEmail(
                email,
                "Welcome in FitTrack",
                "<h1>Hello!</h1><p>Thanks for registration in FitTrack.</p>");
        }

        return result;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
            return null;

        return CreateToken(user);
    }

    private string CreateToken(IdentityUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }
}