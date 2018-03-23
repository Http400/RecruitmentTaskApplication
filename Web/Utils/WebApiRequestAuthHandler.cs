using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Web.Utils
{
    public class WebApiRequestAuthHandler : DelegatingHandler
    {
        IEnumerable<string> authHeaderValues = null;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                request.Headers.TryGetValues("Authorization", out authHeaderValues);
                if (authHeaderValues == null)
                    return base.SendAsync(request, cancellationToken);

                var token = authHeaderValues.FirstOrDefault();
                token = token.Replace("Bearer", "").Trim();
                if (!string.IsNullOrEmpty(token))
                {
                    var principal = AuthenticateJwtToken(token);

                    if (principal != null)
                    {
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }

        protected IPrincipal AuthenticateJwtToken(string token)
        {
            string username;
            string role;

            if (ValidateToken(token, out username, out role))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    //new Claim(ClaimTypes.Role, role)
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return user;
            }

            return null;
        }

        private static bool ValidateToken(string token, out string username, out string role)
        {
            username = null;
            role = null;

            var simplePrinciple = JwtManager.GetPrincipal(token);

            if (simplePrinciple == null)
                return false;

            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null || !identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            //var roleClaim = identity.FindFirst(ClaimTypes.Role);

            username = usernameClaim.Value;
            //role = roleClaim.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }
    }
}