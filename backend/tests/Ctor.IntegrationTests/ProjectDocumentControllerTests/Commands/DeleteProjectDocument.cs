using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Ctor.Application.DTOs;
using Ctor.Application.ProjectDocuments.Commands.DeleteProjectDocument;
using Ctor.Application.ProjectDocuments.Commands.PostProjectDocument;
using Ctor.Application.ProjectDocuments.Queries.GetProjectDocumentByProjectId;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.ProjectDocumentControllerTests.Commands;

public class DeleteProjectDocument : ProjectDocumentControllerFixture
{
    private readonly Testing _testing;

    public DeleteProjectDocument(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Delete_ProjectDocument()
    {
        // Arrange
        int buildingId = 100;
        var building = new Building() { Id = buildingId, BuildingName = "name", ProjectId = 100 };

        await _testing.AddAsync(building);
        await using FileStream file = File.OpenRead("TestPhotos/dummy.png");
        using StreamContent content = new(file);

        var postCommand = new PostProjectDocumentCommand(
            new[] { (await content.ReadAsByteArrayAsync(), file.Name) },
            buildingId,
            Array.Empty<string>());

        var postResult = await _testing.SendAsync(postCommand);

        var query = new DeleteProjectDocumentCommand(postResult[0].Id);

        // Act
        var result = await _testing.SendAsync(query);

        // Assert
        Assert.Equal(postResult[0].Id, result.Id);
    }
}