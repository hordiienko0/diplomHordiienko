using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Services.Commands;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.ServiceControllerTests.GetTests;
public class DeleteServiceTest: ServiceControllerFixture
{
    private readonly Testing _testing;
    public DeleteServiceTest(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Delete_Existing_Building()
    {
        // Arrange
        var entity = new Vendor()
        {
            Id = 100,
            VendorName = "Company",
            Email = "company@ua.fm",
            Website = "company.com",
            Phone = "(066)1234567",
            VendorTypes = new VendorType[] { new VendorType() { Name = "Internet" } }
        };

        await _testing.AddAsync(entity);
        var id = entity.Id;

        // Act
        var command = new DeleteServiceCommand() { Id = id };

        await _testing.SendAsync(command);

        // Assert
        (await _testing.FindAsync<Vendor>(id)).Should().BeNull();
    }
}
