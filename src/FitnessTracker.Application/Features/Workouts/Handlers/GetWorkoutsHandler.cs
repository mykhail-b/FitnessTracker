using FitnessTracker.Application.Features.Workouts.Responses;
using FitTrackApi.Application.Dto;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Application.Features.Workouts.Queries;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class GetWorkoutsHandler : IRequestHandler<GetWorkoutsQuery, PagedListResponse<WorkoutResponse>>
{
    private readonly AppDbContext _db;

    public GetWorkoutsHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedListResponse<WorkoutResponse>> Handle(
        GetWorkoutsQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(1, request.PageNumber);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);
        var userId = await _db.UserProfiles
            .Where(profile => profile.IdentityId == request.IdentityId)
            .Select(profile => (Guid?)profile.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (userId is null)
        {
            return new PagedListResponse<WorkoutResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        var workouts = _db.Workouts
            .AsNoTracking()
            .Where(item => item.UserId == userId);
        var totalCount = await workouts.CountAsync(cancellationToken);
        var items = await workouts
            .OrderByDescending(item => item.Date)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(item => item.WorkoutSets)
            .ToListAsync(cancellationToken);

        return new PagedListResponse<WorkoutResponse>
        {
            Items = items.Select(WorkoutResponse.FromEntity).ToList(),
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
