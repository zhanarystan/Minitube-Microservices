using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Minitube.Upload.Application.Interfaces;

namespace Minitube.Upload.Infrastructure.Services;

public class VideoStorageService : IVideoStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private const string ContainerName = "originals";

    public VideoStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> GenerateUploadSasUrlAsync(Guid videoId)
    {
        var blobName = $"{videoId}.mp4";
        var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        // 1. Get the user delegation key (since we are using Managed Identity)
        var userDelegationKey = await _blobServiceClient.GetUserDelegationKeyAsync(
            DateTimeOffset.UtcNow, 
            DateTimeOffset.UtcNow.AddHours(1));

        // 2. Forming SAS parameters (write only)
        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = ContainerName,
            BlobName = blobName,
            Resource = "b", // b = blob
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Write);

        // 3. Create a token and attach it to the blob's URI
        var sasToken = sasBuilder.ToSasQueryParameters(userDelegationKey, _blobServiceClient.AccountName).ToString();
        
        return $"{blobClient.Uri}?{sasToken}";
    }
}
