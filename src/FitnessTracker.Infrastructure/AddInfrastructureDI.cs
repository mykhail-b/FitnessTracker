using FitnessTracker.Domain.Repository;
using FitnessTracker.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessTracker.Infrastructure;

public static class AddInfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserProfileRepository, AppUserRepository>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IMealRepository, MealRepository>();

        return services;
    }
}
