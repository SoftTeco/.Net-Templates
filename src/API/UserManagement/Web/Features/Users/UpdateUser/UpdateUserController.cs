using Application.Features.Users.UpdateUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Features.Users.UpdateUser;

[ApiController]
[Route("api/users/{userId:guid}")]
public class UpdateUserController : ControllerBase
{
    private readonly UpdateUserHandler _handler;

    public UpdateUserController(UpdateUserHandler handler)
    {
        _handler = handler;
    }

    [HttpPatch]
    public async Task<IResult> UpdateUser([FromRoute] Guid userId, 
        [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _handler.Handle(userId, request, cancellationToken);
        return result.Match<IResult>(
            _ => TypedResults.Ok(),
            _ => TypedResults.NotFound());
    }
}
