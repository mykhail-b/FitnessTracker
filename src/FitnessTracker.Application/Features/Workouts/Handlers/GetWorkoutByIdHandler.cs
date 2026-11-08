using FitnessTracker.Application.Features.Workouts.Queries;
using FitnessTracker.Application.Features.Workouts.Responses;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class GetWorkoutByIdHandler : IRequestHandler<GetWorkoutByIdQuery, WorkoutResponse?>
{
    private readonly AppDbContext _db;

    public GetWorkoutByIdHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<WorkoutResponse?> Handle(
        GetWorkoutByIdQuery request,
        CancellationToken cancellationToken)
    {
        var userId = await _db.UserProfiles
            .Where(profile => profile.IdentityId == request.IdentityId)
            .Select(profile => (Guid?)profile.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (userId is null) return null;

        var workout = await _db.Workouts
            .AsNoTracking()
            .Include(item => item.WorkoutSets)
            .SingleOrDefaultAsync(
                item => item.Id == request.WorkoutId && item.UserId == userId,
                cancellationToken);

        return workout is null ? null : WorkoutResponse.FromEntity(workout);
    }
}
