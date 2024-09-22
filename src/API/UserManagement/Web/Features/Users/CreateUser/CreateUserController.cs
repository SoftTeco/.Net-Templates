using Application.Features.Users.CreateUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Features.Users.CreateUser;

[ApiController]
[Route("api/users")]
public class CreateUserController : ControllerBase
{
    private readonly CreateUserHandler _handler;
    public CreateUserController(CreateUserHandler handler)
    {
        _handler = handler;
    }


    [HttpPost]
    public async Task<IResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        return TypedResults.Ok(await _handler.Handle(request, cancellationToken));
    }
}