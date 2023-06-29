using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Domain.Repositories;
public interface IRepositoryFactory
{
    T GetInstanse<T>();
}
