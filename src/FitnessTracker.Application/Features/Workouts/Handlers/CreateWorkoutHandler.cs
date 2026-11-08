using FitnessTracker.Application.Features.Workouts.Commands;
using FitnessTracker.Application.Features.Workouts.Responses;
using FitnessTracker.Domain.Entity;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class CreateWorkoutHandler : IRequestHandler<CreateWorkoutCommand, WorkoutResponse?>
{
    private readonly AppDbContext _db;

    public CreateWorkoutHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<WorkoutResponse?> Handle(
        CreateWorkoutCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _db.UserProfiles
            .SingleOrDefaultAsync(profile => profile.IdentityId == request.IdentityId, cancellationToken);
        if (user is null) return null;

        var workout = new Workout(Guid.NewGuid(), user.Id);
        _db.Workouts.Add(workout);
        await _db.SaveChangesAsync(cancellationToken);
        return WorkoutResponse.FromEntity(workout);
    }
}
