using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace CarManufacturers.Auth
{
    public class BasicAuth : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuth(
                IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger,
                UrlEncoder encoder,
                ISystemClock clock)
                : base(options, logger, encoder, clock)
        
        {
            
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //verifica si tiene el header 
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            //verifica que tenga un valor 
            string AuthorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(AuthorizationHeader) )
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            //verifica que cumpla el patron: "credential token"
            if (!AuthorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = AuthorizationHeader.Substring(6);
            var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            //el formato es user:pass
            var credentials = credentialAsString.Split(":");
            if (credentials.Length != 2)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var username = credentials[0];
            var password = credentials[1];

            if (username != "green" && password != "light")
            {
                return AuthenticateResult.Fail("Authentication Failed");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username)
            };
            var identity = new ClaimsIdentity(claims, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, password));
        }
    }
}
