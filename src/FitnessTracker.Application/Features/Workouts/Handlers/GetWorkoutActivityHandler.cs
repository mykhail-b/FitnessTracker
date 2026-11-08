using FitnessTracker.Application.Features.Workouts.Queries;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class GetWorkoutActivityHandler : IRequestHandler<GetWorkoutActivityQuery, List<DateOnly>>
{
    private readonly AppDbContext _db;

    public GetWorkoutActivityHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<DateOnly>> Handle(
        GetWorkoutActivityQuery request,
        CancellationToken cancellationToken)
    {
        var userId = await _db.UserProfiles
            .Where(profile => profile.IdentityId == request.IdentityId)
            .Select(profile => (Guid?)profile.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (userId is null) return [];

        var dates = await _db.Workouts
            .AsNoTracking()
            .Where(workout => workout.UserId == userId)
            .Select(workout => workout.Date)
            .ToListAsync(cancellationToken);

        return dates
            .Select(DateOnly.FromDateTime)
            .Distinct()
            .Order()
            .ToList();
    }
}
