using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Ctor.Infrastructure.Services;
public class AzureStorageService : IFileManipulatorService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AzureStorageService> _logger;
    private readonly BlobContainerClient _blobContainerClient;

    public AzureStorageService(IConfiguration configuration, ILogger<AzureStorageService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _blobContainerClient = new BlobContainerClient(
            configuration["AzureFileStorageConnection"], 
            configuration["AzureFilesContainer"]);

    }

    public async Task<List<BlobDTO>> ListAsync()
    { 
        var files = new List<BlobDTO>();
        await foreach (BlobItem file in _blobContainerClient.GetBlobsAsync())
        {
            string uri = _blobContainerClient.Uri.ToString();
            var name = file.Name;
            var fullUri = $"{uri}/{name}";
            files.Add(new BlobDTO
            {
                Uri = fullUri,
                Name = name,
                ContentType = file.Properties.ContentType
            });
        }
        return files;
    }

    public async Task<string?> Save(byte[] fileBytes, string blobName)
    {

        try
        {
            var blob = _blobContainerClient.GetBlobClient(blobName);

            using (var ms = new MemoryStream(fileBytes, false))
            {
                await blob.UploadAsync(ms);
            }

            return blob.Uri.AbsoluteUri;

        }
        catch (RequestFailedException ex)
        when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
        {
            _logger.LogError($"File with name {blobName} already exists in container. Set another name to store the file in the container: '{_blobContainerClient.Name}.'");
        }
        catch (RequestFailedException ex)
        {
            _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
        }
        return null;
    }

    public async Task Delete(string blobFilename)
    {
        BlobClient file = _blobContainerClient.GetBlobClient(blobFilename);
        try
        {
            await file.DeleteAsync();
        }
        catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            _logger.LogError($"File {blobFilename} was not found.");
        }
    }

    public async Task<BlobDTO> DownloadAsync(string blobFilename)
    {
        try
        {
            var file = _blobContainerClient.GetBlobClient(blobFilename);

            if (!await file.ExistsAsync())
            {
                _logger.LogError($"File {blobFilename} was not found.");
                return null;
            }

            var data = await file.OpenReadAsync();
            Stream blobContent = data;
            var content = await file.DownloadContentAsync();
            string name = blobFilename;
            string contentType = content.Value.Details.ContentType;
            return new BlobDTO { Content = blobContent, Name = name, ContentType = contentType };
            
        }
        catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            _logger.LogError($"File {blobFilename} was not found.");
        }

        return null;

    }
}
