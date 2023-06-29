using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;

namespace Ctor.Infrastructure.Services;
public class CompanyIdGeneratorService : ICompanyIdGeneratorService
{
    private readonly IApplicationDbContext _context;
    public CompanyIdGeneratorService(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<long> GenerateNewCompanyId()
    {
        var ids = (await _context.Companies.GetAll()).Select(el => el.CompanyId).ToList();
        long newId = -1;
        Random rnd = new Random();
        do
        {
            newId = rnd.NextInt64(100000, 9999999);
        } while (ids.Contains(newId));
        return newId;
    }
}
