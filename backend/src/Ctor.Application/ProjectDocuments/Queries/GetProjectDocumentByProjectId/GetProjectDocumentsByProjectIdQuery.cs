using System.Linq.Expressions;
using AutoMapper;
using Ctor.Application.Common.Enums;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.DTOs;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.ProjectDocuments.Queries.GetProjectDocumentByProjectId;

public record GetProjectDocumentsByProjectIdQuery
    (long ProjectId, long? BuildingId, QueryModelDTO? QueryModel) : IRequest<List<GetProjectDocumentByProjectIdResponseDto>>;

public class
    GetProjectDocumentsByProjectIdQueryHandler : IRequestHandler<GetProjectDocumentsByProjectIdQuery,
        List<GetProjectDocumentByProjectIdResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetProjectDocumentsByProjectIdQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<GetProjectDocumentByProjectIdResponseDto>> Handle(
        GetProjectDocumentsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        string query = request.QueryModel.Query;
        Expression<Func<ProjectDocument, bool>> queryPredicate = document =>
            (string.IsNullOrEmpty(query) || document.Document.Name.ToLower().Contains(query))
            && ((!request.BuildingId.HasValue || document.BuildingId == request.BuildingId) && (!document.ProjectId.HasValue || request.ProjectId == document.ProjectId));

        var projectDocuments =
            await _context.ProjectDocuments.GetOrdered<GetProjectDocumentByProjectIdResponseDto>(
                request.QueryModel.Sort, request.QueryModel.Order, queryPredicate);
        return projectDocuments;
    }
}