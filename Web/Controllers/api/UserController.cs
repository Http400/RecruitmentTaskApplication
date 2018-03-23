using System;
using System.Net;
using System.Web.Http;
using Web.Models;
using Web.Services.Interfaces;
using Web.Utils;

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
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "An error occured. Please try again.");
            }
        }

        [HttpPost]
        public IHttpActionResult SignIn(SignInDTO signInData)
        {
            try
            {
                var result = _userService.ValidateUser(signInData.Email, signInData.Password);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "Wrong credentials.");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "An error occured. Please try again.");
            }
        }

        [HttpPost]
        public IHttpActionResult SignUp(SignUpDTO signUpData)
        {
            try
            {
                var user = _userService.CreateUser(signUpData.Email, signUpData.Password);
                if (user != null)
                    return Ok("Your account has been created.");
                else
                    throw new Exception();
            }
            catch (UserAlreadyExistsException ex)
            {
                return Content(HttpStatusCode.BadRequest, "User with this email already exists.");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "An error occured. Please try again.");
            }
        }
    }
}