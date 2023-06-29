using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Buildings.Queries.GetBuildingsListByProjectId;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.Buildings.Queries;
public class GetBuildingsByProjectId : BuildingCotrollerFixture
{
    private readonly Testing _testing;

    public GetBuildingsByProjectId(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Get_Buildings_ByProjectId()
    {
        // Arrange
        var projectId = 1;
        var buildings = new List<Building>
        {
            new Building()
            {
                Id = 50,
                ProjectId = projectId,
                BuildingName = "Test Building",
                BuildingBlocks = new List<BuildingBlock>()
                {
                    new BuildingBlock()
                    {
                        BuildingBlockName = "Test Building Block",
                        Details = ""
                    }
                }
            },
            new Building()
            {
                Id = 51,
                ProjectId = projectId,
                BuildingName = "Test Building",
                BuildingBlocks = new List<BuildingBlock>()
                {
                    new BuildingBlock()
                    {
                        BuildingBlockName = "Test Building Block",
                        Details = ""
                    }
                }
            },
        };

        await _testing.AddAsync(buildings[0]);
        await _testing.AddAsync(buildings[1]);

        var query = new GetBuildingsListByProjectIdQuery(projectId);

        // Act 
        var result = await _testing.SendAsync(query);

        // Assert
        result.Should().HaveCountGreaterThanOrEqualTo(2);
    }
}
