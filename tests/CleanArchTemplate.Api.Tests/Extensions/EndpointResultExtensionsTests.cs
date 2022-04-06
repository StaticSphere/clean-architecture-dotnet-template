using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using CleanArchTemplate.Api.Extensions;
using CleanArchTemplate.Application.Models;
using CleanArchTemplate.Application.Models.Enumerations;
using Xunit;

namespace CleanArchTemplate.Api.Tests.Extensions;

public class EndpointResultExtensionsTests
{
    [Fact]
    public void ToActionResultReturnsOkResultOnSuccess()
    {
        var result = new EndpointResult(EndpointResultStatus.Success).ToActionResult();

        result.Should().NotBeNull().And.BeOfType<OkResult>();
    }

    [Fact]
    public void ToActionResultReturnsNotFoundResultOnNotFound()
    {
        var result = new EndpointResult(EndpointResultStatus.NotFound).ToActionResult();

        result.Should().NotBeNull().And.BeOfType<NotFoundResult>();
    }

    [Fact]
    public void ToActionResultReturnsUnprocessableEntityResultOnInvalid()
    {
        var result = new EndpointResult(EndpointResultStatus.Invalid).ToActionResult();

        result.Should().NotBeNull().And.BeOfType<UnprocessableEntityResult>();
    }

    [Fact]
    public void ToActionResultReturnsUnprocessableEntityObjectResultOnInvalidWithData()
    {
        var result = new EndpointResult(EndpointResultStatus.Invalid) { Messages = new[] { "Oops" } }.ToActionResult();

        result.Should().NotBeNull().And.BeOfType<UnprocessableEntityObjectResult>();
    }

    [Fact]
    public void ToActionResultReturnsInvalidDataDetailsOnInvalidWithData()
    {
        var result = new EndpointResult(EndpointResultStatus.Invalid) { Messages = new[] { "Oops" } }.ToActionResult();

        result.As<UnprocessableEntityObjectResult>().Value.Should().NotBeNull();
        result.As<UnprocessableEntityObjectResult>().Value.As<IEnumerable<string>>().Should().HaveCount(1);
    }

    [Fact]
    public void ToActionResultReturnsConflictResultOnDuplicate()
    {
        var result = new EndpointResult(EndpointResultStatus.Duplicate).ToActionResult();

        result.Should().NotBeNull().And.BeOfType<ConflictResult>();
    }

    [Fact]
    public void ToActionResultReturnsUnauthorizedEntityResultOnUnauthorized()
    {
        var result = new EndpointResult(EndpointResultStatus.Unauthorized).ToActionResult();

        result.Should().NotBeNull().And.BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public void ToActionResultReturnsGoneStatusCodeResultOnGone()
    {
        var result = new EndpointResult(EndpointResultStatus.Gone).ToActionResult();

        result.Should().NotBeNull().And.BeOfType<StatusCodeResult>();
        result.As<StatusCodeResult>().StatusCode.Should().Be((int)HttpStatusCode.Gone);
    }

    [Fact]
    public void ToActionResultReturnsServerErrorStatusCodeResultOnAnythingElse()
    {
        var result = new EndpointResult(EndpointResultStatus.Error).ToActionResult();

        result.Should().NotBeNull().And.BeOfType<StatusCodeResult>();
        result.As<StatusCodeResult>().StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }

    [Fact]
    public void ToActionResultReturnsOkObjectResultOnSuccessWithData()
    {
        var result = new EndpointResult<string>("It works!").ToActionResult();

        result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
    }

    [Fact]
    public void ToActionResultReturnsDataOnSuccessWithData()
    {
        var result = new EndpointResult<string>("It works!").ToActionResult();

        result.As<OkObjectResult>().Value.Should().BeOfType<string>().And.Be("It works!");
    }
}
