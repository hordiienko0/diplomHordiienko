using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Buildings.Commands.DeleteBuilding;
using Ctor.Domain.Entities;
using Ctor.IntegrationTests.CompanyControllerTests;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.Buildings.Commands;
public class DeleteBuilding : CompanyControllerFixture
{
    private readonly Testing _testing;

    public DeleteBuilding(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Delete_Existing_Building()
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

        // Act
        var command = new DeleteBuildingCommand(id);

        await _testing.SendAsync(command);

        // Assert
        (await _testing.FindAsync<Building>(id)).Should().BeNull();
    }
}
