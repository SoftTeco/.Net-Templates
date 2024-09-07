using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Data;
using UserManagement.Domain.Entities;

namespace UserManagement.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Users : ControllerBase
    {
        private readonly IUsersDbContext _usersDbContext;

        public Users(IUsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        [HttpGet]
        public IResult GetAll()
        {
            //ToDo: replace domain model by response model

            return TypedResults.Ok(_usersDbContext.Users.ToList());
        }
    }
}