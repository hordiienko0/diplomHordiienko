using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Buildings.Commands.AddBuilding;
using Ctor.Application.Common.Exceptions;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.Buildings.Commands;
public class AddBuilding : BuildingCotrollerFixture
{
    private readonly Testing _testing;

    public AddBuilding(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Add_Valid_Building()
    {
        // Arrange 

        var command = new AddBuildingCommand()
        {
            BuildingName = "Valid name",
            ProjectId = 1
        };

        // Act

        var result = await _testing.SendAsync(command);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Add_Invalid_Building()
    {
        // Arrange 

        var command = new AddBuildingCommand()
        {
            BuildingName = "",
            ProjectId = 1
        };

        // Act
        // Assert
        await FluentActions.Invoking(() => _testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}
