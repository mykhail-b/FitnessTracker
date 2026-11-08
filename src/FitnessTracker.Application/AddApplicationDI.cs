using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessTracker.Application;

public static class AddApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(AddApplicationDI).Assembly));

        return services;
    }
}
