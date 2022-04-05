using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using CleanArchTemplate.Api.Exceptions;
using CleanArchTemplate.Api.Services;
using Xunit;

namespace CleanArchTemplate.Api.Tests.Services;

public class PrincipalServiceTests
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessor;

    public PrincipalServiceTests()
    {
        _httpContextAccessor = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
    }

    [Fact]
    public void UserIdThrowsMissingClaimsPrincipleExceptionWhenClaimsPrincipalNotFound()
    {
        _httpContextAccessor.SetupGet(x => x.HttpContext).Returns((HttpContext)null!);
        var service = new PrincipalService(_httpContextAccessor.Object);

        var action = () => service.UserId;

        action.Should().Throw<MissingClaimsPrincipalException>();
    }

    [Fact]
    public void UserIdReturnsIdWhenNameIdentifierClaimExists()
    {
        _httpContextAccessor.SetupGet(x => x.HttpContext!.User).Returns(new ClaimsPrincipal(new List<ClaimsIdentity>(new[]{
            new ClaimsIdentity(new List<Claim>(new []{ new Claim(ClaimTypes.NameIdentifier, "3")}))
        })));
        var service = new PrincipalService(_httpContextAccessor.Object);

        var result = service.UserId;

        result.Should().Be(3);
    }
}
