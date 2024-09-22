using UserManagement.Application.Data;
using UserManagement.Domain.Entities;

namespace Application.Features.Users.CreateUser;

public class CreateUserHandler
{
   private readonly IUsersDbContext _usersDbContext;

    public CreateUserHandler(IUsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User{Id = Guid.NewGuid(), FirstName = request.FirstName, LastName = request.LastName};

        _usersDbContext.Users.Add(user);
        await _usersDbContext.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse{ Id = user.Id };
    }
}