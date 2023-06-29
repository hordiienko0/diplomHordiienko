using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ctor.Infrastructure.Services;

public class FileManipulatorService : IFileManipulatorService
{
    private readonly string _folderPath;
    private readonly ILogger _logger;

    public FileManipulatorService(FileManipulatorSettings fileSettings, ILogger<FileManipulatorService> logger)
    {
        _folderPath = fileSettings.FolderPath;
        _logger = logger;
    }
    public async Task<string?> Save(byte[] fileData, string fileName)
    {
        var filePath = Path.Combine(_folderPath, $"{fileName}");
        var directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        try
        {
            await using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.WriteAsync(fileData, 0, fileData.Length);
            }
            return fileName.Replace("\\", "/");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, "Something went wrong with file upload");
        }

        return null;
       
    }

    public async Task Delete(string fileName)
    {
        var filePath = Path.Combine(_folderPath, $"{fileName}");
        await Task.Run(() => File.Delete(filePath));
    }
}