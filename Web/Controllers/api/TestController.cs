using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Web.Controllers.api
{
    public class TestController : ApiController
    {

        public IHttpActionResult GetTest(int number)
        {
            if (number < 5)
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "An error occured. Please try again.");
            }
        }
    }
}