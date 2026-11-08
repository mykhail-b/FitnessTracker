using System;

namespace FitTrackApi.Server.Services;

public interface IEmailService
{
    Task SendEmail(string toEmail, string subject, string body, CancellationToken cancellationToken = default);
}
