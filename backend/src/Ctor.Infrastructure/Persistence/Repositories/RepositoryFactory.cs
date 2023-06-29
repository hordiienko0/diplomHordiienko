using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ctor.Infrastructure.Persistence.Repositories;
public class RepositoryFactory: IRepositoryFactory
{
    private readonly IServiceProvider _services;
    public RepositoryFactory(IServiceProvider services)
    {
        _services = services;
    }
    public T GetInstanse<T>()
    {
        var service = _services.GetService<T>();
        return service;
    }

}
