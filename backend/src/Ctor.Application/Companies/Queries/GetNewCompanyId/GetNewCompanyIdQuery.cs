using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Companies.Queries.GetNewCompanyId;

public record GetNewCompanyIdQuery() : IRequest<long>;
public class GetNewCompanyIdHandler : IRequestHandler<GetNewCompanyIdQuery, long>
{
    private readonly ICompanyIdGeneratorService _generatorService;
    public GetNewCompanyIdHandler(ICompanyIdGeneratorService generatorService)
    {
        this._generatorService = generatorService;
    }

    public async Task<long> Handle(GetNewCompanyIdQuery request, CancellationToken cancellationToken)
    {
        return await this._generatorService.GenerateNewCompanyId();
    }
}

