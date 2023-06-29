namespace Ctor.Application.Common.Interfaces;

public interface IFileManipulatorService
{
    public Task<string?> Save(byte[] fileData, string fileName);
    public Task Delete(string fileName);
}