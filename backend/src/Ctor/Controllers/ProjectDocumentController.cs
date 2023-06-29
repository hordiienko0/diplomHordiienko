using System.Collections.Concurrent;
using Ctor.Application.DTOs;
using Ctor.Application.ProjectDocuments.Commands.DeleteProjectDocument;
using Ctor.Application.ProjectDocuments.Commands.PostProjectDocument;
using Ctor.Application.ProjectDocuments.Commands.PutProjectDocument;
using Ctor.Application.ProjectDocuments.Queries.GetProjectDocumentByProjectId;
using Ctor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;

[Route("api/projectDocuments")]
[ApiExceptionFilter]
public class ProjectDocumentController : ApiControllerBase
{
    [HttpGet("project/{id:long}")]
    public async Task<ActionResult<List<GetProjectDocumentByProjectIdResponseDto>>> GetProjectDocumentsByProjectId(
        long id, long? buildingId, [FromQuery] QueryModelDTO queryModel)
    {
        return await Mediator.Send(new GetProjectDocumentsByProjectIdQuery(id, buildingId, queryModel));
    }

    [HttpPost("building/{id:long}")]
    public async Task<ActionResult<List<PostProjectDocumentResponseDto>>> PostProjectDocument(
        [FromForm] IFormCollection data,
        long id,
        CancellationToken ct)
    {
        var urls = data["url"].ToArray() ?? Array.Empty<string>();
        var files = new ConcurrentBag<(byte[] Data, string FileName)>();

        await Parallel.ForEachAsync(data.Files, ct, async (file, ct2) =>
        {
            ct2.ThrowIfCancellationRequested();
            files.Add((await file.GetBytes(), file.FileName));
        });

        return await Mediator.Send(new PostProjectDocumentCommand(files.ToArray(), id, urls), ct);
    }

    [HttpPut]
    public async Task<ActionResult<PutProjectDocumentResponseDto>> PutProjectDocument(
        [FromBody] PutProjectDocumentRequestDto data)
    {
        return await Mediator.Send(new PutProjectDocumentCommand(data));
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<DeleteProjectDocumentResponseDto>> DeleteProjectDocument(long id)
    {
        return await Mediator.Send(new DeleteProjectDocumentCommand(id));
    }
}