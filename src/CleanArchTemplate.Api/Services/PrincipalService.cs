using System.Globalization;
using System.Security.Claims;
using CleanArchTemplate.Api.Exceptions;
using CleanArchTemplate.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace CleanArchTemplate.Api.Services
{
    public class PrincipalService : IPrincipalService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PrincipalService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                // TODO: Uncomment the following code if Authentication has been set up to enable getting the current users ID.

                /*
                var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                if (claimsPrincipal == null)
                    throw new MissingClaimsPrincipalException();

                return int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0", CultureInfo.CurrentCulture);
                */
                return 1;
            }
        }
    }
}
