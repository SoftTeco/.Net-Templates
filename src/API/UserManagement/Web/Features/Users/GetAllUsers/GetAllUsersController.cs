using Application.Features.Users.GetAllUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Features.Users.GetAllUsers;

[ApiController]
[Route("api/users")]
public class GetAllUsersController : ControllerBase
{
    private readonly GetAllUsersHandler _handler;

    public GetAllUsersController(GetAllUsersHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public async Task<IResult> GetAll()
    {
        return TypedResults.Ok(await _handler.Handle());
    }
}