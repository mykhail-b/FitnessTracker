using FitnessTracker.Application.Features.Workouts.Queries;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class GetWorkoutActivityHandler : IRequestHandler<GetWorkoutActivityQuery, List<DateOnly>>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public GetWorkoutActivityHandler(
        IWorkoutRepository workoutRepository,
        IUserProfileRepository userProfileRepository)
    {
        _workoutRepository = workoutRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<List<DateOnly>> Handle(
        GetWorkoutActivityQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return [];

        var dates = await _workoutRepository.GetActivityDatesAsync(user.Id, cancellationToken);
        return dates.ToList();
    }
}
