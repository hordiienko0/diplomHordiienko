using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Ctor.Infrastructure.Core;
public class LazyInstance<T> : Lazy<T>
{
    public LazyInstance(IServiceProvider serviceProvider)
        : base(() => serviceProvider.GetRequiredService<T>())
    {

    }
}
