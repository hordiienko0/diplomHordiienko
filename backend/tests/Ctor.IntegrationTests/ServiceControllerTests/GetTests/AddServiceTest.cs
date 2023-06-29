using System;
using System.Collections.Generic;
using Ctor.Application.Common.Exceptions;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Services;
using Ctor.Application.Services.Commands;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Ctor.IntegrationTests.ServiceControllerTests.GetTests;
public class AddServiceTest:ServiceControllerFixture
{
    private readonly Testing _testing;

    public AddServiceTest(Testing testing)
    {       
        _testing = testing;
    }

    [Fact]
    public async Task Add_Valid_Service()
    {
        var command = new AddServiceCommand()
        {
            Company = "Company",
            Email = "email@ua.fm",
            Website = "company.com",
            Phone = "(066)1234567",
            Types = new string[] { "Internet" }
        };

        // Act

        var result = await _testing.SendAsync(command);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Add_InvalidEmail_Service()
    {
        var command = new AddServiceCommand()
        {
            Company = "Company",
            Email = "email@",
            Website = "company.com",
            Phone = "(066)1234567",
            Types = new string[] { "Internet" }
        };

        //Act
        // Assert
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    [Fact]
    public async Task Add_InvalidPhone_Service()
    {    
        var command = new AddServiceCommand()
        {
            Company = "Company",
            Email = "email@ua.fm",
            Website = "company.com",
            Phone = "(066)123456712312313123131231231231323",
            Types = new string[] { "Internet" }
        };

        //Act
        // Assert
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    [Fact]
    public async Task Add_InvalidTypes_Service()
    {
        var command = new AddServiceCommand()
        {
            Company = "Company",
            Email = "email@ua.fm",
            Website = "company.com",
            Phone = "(066)1234567",
            Types = new string[] {}
        };

        //Act
        // Assert
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}
