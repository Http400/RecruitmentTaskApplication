using System;
using System.Net;
using System.Web;
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

        [HttpGet]
        public IHttpActionResult GetUser()
        {
            try
            {
                var user = _userService.GetUser(CurrentUser());
                return Ok(user);
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
                var user = _userService.CreateUser(signUpData);
                if (user != null)
                    return Ok("Your account has been created. Now You can sign in.");
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

        [HttpPut]
        public IHttpActionResult EditUser(UserDTO userData)
        {
            try
            {
                _userService.EditUser(CurrentUser(), userData);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "An error occured. Please try again.");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangePassword(ChangePasswordDTO passwordData)
        {
            try
            {
                _userService.ChangePassword(CurrentUser(), passwordData.CurrentPassword, passwordData.NewPassword);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "An error occured. Please try again.");
            }
        }

        protected int CurrentUser()
        {
            try
            {
                var identity = GetCurrentCallIdentity();
                if (identity == null)
                    throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);

                var userId = _userService.GetUserId(identity);
                if (userId != null)
                    return userId;
                else
                    throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                // log
                throw;
            }
        }

        protected System.Security.Principal.IIdentity GetCurrentCallIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
                return HttpContext.Current.User.Identity;
            return null;
        }
    }
}