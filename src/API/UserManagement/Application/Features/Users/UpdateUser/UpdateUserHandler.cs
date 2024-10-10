using Microsoft.EntityFrameworkCore;
using OneOf.Types;
using OneOf;
using UserManagement.Application.Data;

namespace Application.Features.Users.UpdateUser;

public class UpdateUserHandler
{
    private readonly IUsersDbContext _usersDbContext;

    public UpdateUserHandler(IUsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<OneOf<Success, NotFound>> Handle(Guid userId, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersDbContext.Users
            .SingleOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, cancellationToken);
        if (user == null)
        {
            return new NotFound();
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email; 
        await _usersDbContext.SaveChangesAsync(cancellationToken);
        return new Success();
    }
}
