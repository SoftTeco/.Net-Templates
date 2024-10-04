using System.Diagnostics.Metrics;
using UserManagement.Application.Data;
using UserManagement.Domain.Entities;

namespace Application.Features.Users.CreateUser;

public class CreateUserHandler
{
    private readonly IUsersDbContext _usersDbContext;
    private readonly Meter _usersMeter;
    private readonly Counter<int> _userCreatedCounter;

    public CreateUserHandler(IUsersDbContext usersDbContext, IMeterFactory meterFactory)
    {
        _usersDbContext = usersDbContext;

        _usersMeter = meterFactory.Create(Meters.Users);
        _userCreatedCounter = _usersMeter.CreateCounter<int>("users.created.count");
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {

        var user = new User { Id = Guid.NewGuid(), FirstName = request.FirstName, LastName = request.LastName };

        _usersDbContext.Users.Add(user);
        await _usersDbContext.SaveChangesAsync(cancellationToken);

        _userCreatedCounter.Add(1);

        return new CreateUserResponse { Id = user.Id };
    }
}