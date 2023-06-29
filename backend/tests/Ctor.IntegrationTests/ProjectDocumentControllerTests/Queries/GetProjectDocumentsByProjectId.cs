using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ctor.Application.DTOs;
using Ctor.Application.ProjectDocuments.Queries.GetProjectDocumentByProjectId;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.ProjectDocumentControllerTests.Queries;

public class GetProjectDocumentsByProjectId : ProjectDocumentControllerFixture
{
    private readonly Testing _testing;

    public GetProjectDocumentsByProjectId(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Get_ProjectDocuments_ByProjectId()
    {
        // Arrange
        var projectId = 100;
        var building = new Building() {Id = 100, BuildingName = "name", ProjectId = projectId};
        var documents = new List<ProjectDocument>
        {
            new ProjectDocument()
            {
                Id = 100,
                BuildingId = 100,
                Created = DateTime.UtcNow,
                Document = new Document(){ Link = "link/link.png", Name = "link.png", Path = "path/path"}
            },
            new ProjectDocument()
            {
                Id = 101,
                BuildingId = 100,
                Created = DateTime.UtcNow,
                Document = new Document(){ Link = "link/link2.png", Name = "link2.png", Path = "path/path"}
            }
        };
        await _testing.AddAsync(building);
        await _testing.AddAsync(documents[0]);
        await _testing.AddAsync(documents[1]);

        var query = new GetProjectDocumentsByProjectIdQuery(projectId, 100, new QueryModelDTO { Sort = "created" });

        // Act
        var result = await _testing.SendAsync(query);

        // Assert
        result.Should().HaveCountGreaterThanOrEqualTo(2);
    }
}