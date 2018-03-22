using System.Net;
using System.Web.Http;
using Web.Services.Interfaces;

namespace Web.Controllers.api
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(users);
            }
            catch
            {
                return Content(HttpStatusCode.InternalServerError, "An error occured. Please try again.");
            }
        }
    }
}