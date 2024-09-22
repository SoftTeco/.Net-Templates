using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Data;
using UserManagement.Domain.Entities;

namespace Application.Features.Users.GetAllUsers;

public class GetAllUsersHandler
{
   private readonly IUsersDbContext _usersDbContext;

    public GetAllUsersHandler(IUsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<IEnumerable<UserResponse>> Handle()
    {
        return (await _usersDbContext.Users.ToListAsync()).Select(ToResponse);
    }

    private UserResponse ToResponse(User user)
    {
        return new UserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}