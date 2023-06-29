using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ctor.Application.DTOs;
using Ctor.Application.ProjectDocuments.Commands.PutProjectDocument;
using Ctor.Application.ProjectDocuments.Queries.GetProjectDocumentByProjectId;
using Ctor.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ctor.IntegrationTests.ProjectDocumentControllerTests.Commands;

public class PutProjectDocument : ProjectDocumentControllerFixture
{
    private readonly Testing _testing;

    public PutProjectDocument(Testing testing)
    {
        _testing = testing;
    }

    [Fact]
    public async Task Put_ProjectDocument()
    {
        // Arrange
        var id = 100;
        var building = new Building() {Id = 100, BuildingName = "name", ProjectId = 100};
        var projectDocument = new ProjectDocument()
        {
            Id = id,
            BuildingId = 100,
            Created = DateTime.UtcNow,
            Document = new Document() { Link = "link/link.png", Name = "link.png", Path = "path/path" }
        };

        await _testing.AddAsync(building);
        await _testing.AddAsync(projectDocument);

        var query = new PutProjectDocumentCommand(new PutProjectDocumentRequestDto(){Id = id, Name = "newName"});

        // Act
        var result = await _testing.SendAsync(query);

        // Assert
        Assert.Equal(projectDocument.Id, result.Id);
        Assert.NotEqual(result.FileName, projectDocument.Document.Name);
    }
}