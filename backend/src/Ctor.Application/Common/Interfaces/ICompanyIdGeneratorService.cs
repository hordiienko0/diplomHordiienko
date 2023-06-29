using System.Threading.Tasks;

namespace Ctor.Application.Common.Interfaces;
public interface ICompanyIdGeneratorService
{
    public Task<long> GenerateNewCompanyId();
}
