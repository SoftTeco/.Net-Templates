using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using UserManagement.Application.Data;

namespace Application.Features.Users.DeleteUser;

public class DeleteUserHandler
{
    private readonly IUsersDbContext _usersDbContext;

    public DeleteUserHandler(IUsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<OneOf<Success, NotFound>> Handle(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _usersDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
        {
            return new NotFound();
        }

        _usersDbContext.Users.Remove(user);
        await _usersDbContext.SaveChangesAsync(cancellationToken);
        return new Success();
    }
}
