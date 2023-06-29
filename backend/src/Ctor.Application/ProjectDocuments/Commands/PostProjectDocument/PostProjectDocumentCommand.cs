using System.Collections.Concurrent;
using AutoMapper;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ctor.Application.ProjectDocuments.Commands.PostProjectDocument;

public record PostProjectDocumentCommand(
    ICollection<(byte[] Data, string FileName)> Files,
    long BuildingId,
    string[] Urls) : IRequest<List<PostProjectDocumentResponseDto>>;

public class PostProjectDocumentCommandHandler
    : IRequestHandler<PostProjectDocumentCommand, List<PostProjectDocumentResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IFileManipulatorService _fileManipulatorService;
    private readonly IDateTime _dateTime;
    private readonly ILogger<PostProjectDocumentCommandHandler> _logger;

    public PostProjectDocumentCommandHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IFileManipulatorService fileManipulatorService,
        IDateTime dateTime,
        ILogger<PostProjectDocumentCommandHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _fileManipulatorService = fileManipulatorService;
        _dateTime = dateTime;
        _logger = logger;
    }

    public async Task<List<PostProjectDocumentResponseDto>> Handle(PostProjectDocumentCommand request,
        CancellationToken cancellationToken)
    {
        // Check if exists
        var building = await _context.Buildings.GetById(request.BuildingId, cancellationToken);
        var files = new ConcurrentBag<(byte[] Data, string FileName)>(request.Files);

        await Parallel.ForEachAsync(request.Urls.Where(u => !string.IsNullOrWhiteSpace(u)), cancellationToken,
            async (url, ct) =>
            {
                try
                {
                    using var client = new HttpClient();
                    using var response = await client.GetAsync(url, ct).ConfigureAwait(false);
                    var bytes = await response.Content.ReadAsByteArrayAsync(ct).ConfigureAwait(false);
                    files.Add((bytes, url));
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Ignoring error an error while downloading file from url '{@Url}'", url);
                }
            });

        var projectDocuments = new List<ProjectDocument>();

        foreach (var file in files)
        {
            var fileType = Path.GetExtension(file.FileName);
            var path = Path.Combine("projectDocuments", $"{Guid.NewGuid()}{fileType}");

            var link = await _fileManipulatorService.Save(file.Data, path);

            if (link == null)
                throw new IOException();

            var projectDocument = new ProjectDocument
            {
                Building = building,
                Created = _dateTime.UtcNow,
                Document = new Document { Link = link, Path = path, Name = file.FileName, },
            };

            projectDocuments.Add(projectDocument);
        }

        await _context.ProjectDocuments.AddRangeAsync(projectDocuments);
        await _context.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<List<PostProjectDocumentResponseDto>>(projectDocuments);
        return response;
    }
}