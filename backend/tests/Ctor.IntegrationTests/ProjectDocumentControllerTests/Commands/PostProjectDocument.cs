using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Ctor.Application.ProjectDocuments.Commands.DeleteProjectDocument;
using Ctor.Application.ProjectDocuments.Commands.PostProjectDocument;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.ProjectDocumentControllerTests.Commands;

public class PostProjectDocument : ProjectDocumentControllerFixture
{
    private readonly Testing _testing;

    public PostProjectDocument(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Delete_ProjectDocument()
    {
        // Arrange
        var building = new Building { BuildingName = "name", ProjectId = 100 };

        await _testing.AddAsync(building);

        await using FileStream file = File.OpenRead("TestPhotos/dummy.png");
        using StreamContent content = new(file);

        var postCommand = new PostProjectDocumentCommand(
            new[] { (await content.ReadAsByteArrayAsync(), file.Name) },
            building.Id,
            new[] { "https://icatcare.org/app/uploads/2018/07/Thinking-of-getting-a-cat.png" });

        // Act
        var postResult = await _testing.SendAsync(postCommand);

        // Assert
        postResult.Should().NotBeNullOrEmpty();
        postResult.Should().HaveCount(2);
    }
}