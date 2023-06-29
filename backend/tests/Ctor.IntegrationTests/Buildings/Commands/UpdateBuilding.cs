using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Buildings.Commands.UpdateBuilding;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.Buildings.Commands;
public class UpdateBuilding : BuildingCotrollerFixture
{
    private readonly Testing _testing;

    public UpdateBuilding(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Update_Existing_Building()
    {
        // Arrange
        var entity = new Building()
        {
            Id = 50,
            ProjectId = 1,
            BuildingName = "Test Building",
            BuildingBlocks = new List<BuildingBlock>()
            {
                new BuildingBlock()
                {
                    BuildingBlockName = "Test Building Block",
                    Details = ""
                }
            }
        };

        await _testing.AddAsync(entity);
        var id = entity.Id;
        var expectedBuildingName = "New Building Name";

        // Act

        var command = new UpdateBuildingCommand()
        {
            Id = id,
            BuildingName = expectedBuildingName
        };
        await _testing.SendAsync(command);

        // Assert 

        var resultEntity = await _testing.FindAsync<Building>(id);

        resultEntity.Should().NotBeNull();
        resultEntity?.BuildingName.Should().Be(expectedBuildingName);
    }
}
