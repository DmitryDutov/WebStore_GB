using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity
{
    [ApiController]
    [Route(WebAPIAdresses.Identity.Users)]
    public class UsersAPIController : ControllerBase
    {
    }

    [ApiController]
    [Route(WebAPIAdresses.Identity.Roles)]
    public class RolesAPIController : ControllerBase
    {
    }
}
