using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Services;
using Ctor.Application.Services.Commands;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.ServiceControllerTests.GetTests;
public class EditServiceTest : ServiceControllerFixture
{
    private readonly Testing _testing;
    public EditServiceTest(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Edit_Valid_Service()
    {
        var entity = new Vendor()
        {
            Id = 100,
            VendorName = "Company",
            Email = "company@ua.fm",
            Website = "company.com",
            Phone = "(066)1234567",
            VendorTypes = new VendorType[] { new VendorType() { Name = "Internet"} }
        };
        await _testing.AddAsync(entity);

        long id = entity.Id;
        string expectedCompany = "New Company";

        var command = new EditServiceCommand()
        {
            Id = id,
            Company = expectedCompany,
            Email = "email@ua.fm",
            Website = "company.com",
            Phone = "(066)1234567",
            Types = new string[] { "Internet" }
        };

        // Act

        await _testing.SendAsync(command);

        // Assert

        var resultEntity = await _testing.FindAsync<Vendor>(id);
        resultEntity.Should().NotBeNull();
        resultEntity?.VendorName.Should().Be(expectedCompany);
    }

    [Fact]
    public async Task Edit_InvalidWebsite_Service()
    {
        var command = new EditServiceCommand()
        {
            Id = 1,
            Company = "Company",
            Email = "email@ua.fm",
            Website = "company",
            Phone = "(066)1234567",
            Types = new string[] { "Internet" }
        };

        //Act
        // Assert
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Edit_InvalidId_Service()
    {
        var command = new EditServiceCommand()
        {
            Company = "Company",
            Email = "email@ua.fm",
            Website = "company",
            Phone = "(066)1234567",
            Types = new string[] { "Internet" }
        };

        //Act
        // Assert
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}
