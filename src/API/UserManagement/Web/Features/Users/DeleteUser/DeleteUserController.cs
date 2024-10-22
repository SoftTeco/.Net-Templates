using Application.Features.Users.DeleteUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Features.Users.DeleteUser;

[ApiController]
[Route("api/users/{userId:guid}")]
public class DeleteUserController : ControllerBase
{
    private readonly DeleteUserHandler _handler;

    public DeleteUserController(DeleteUserHandler handler)
    {
        _handler = handler;
    }

    [HttpDelete]
    public async Task<IResult> DeleteUser([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = await _handler.Handle(userId, cancellationToken);
        return result.Match<IResult>(
            _ => TypedResults.NoContent(),
            _ => TypedResults.NotFound());
    }
}
