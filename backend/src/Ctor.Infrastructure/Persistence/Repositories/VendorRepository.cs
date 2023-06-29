using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ctor.Infrastructure.Persistence.Repositories;
public class VendorRepository:GenericRepository<Vendor>, IVendorRepository
{
    public VendorRepository(ApplicationDbContext _context, Lazy<IMapper> mapper) : base(_context, mapper)
    {
    }

    public Task<Vendor> GetByIdWithVendorTypes(long id)
    {
       return _context.Set<Vendor>().Include(x => x.VendorTypes).FirstOrDefaultAsync(v=>v.Id==id);
    }
}
