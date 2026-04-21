using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minitube.Upload.Application.Interfaces;
using Minitube.Upload.Domain.Entities;

namespace Minitube.Upload.Application.Services;

public class VideoService : IVideoService
{
    public readonly IVideoMetadataRepository _videoMetadaRepository;
    public readonly IVideoStorageService _storageService;

    public VideoService(IVideoMetadataRepository videoMetadaRepository, IVideoStorageService storageService)
    {
        _videoMetadaRepository = videoMetadaRepository;
        _storageService = storageService;
    }
    
    public async Task<string> Reserve(VideoMetadata videoMetadata)
    {
        var videoId = Guid.NewGuid();

        // Получаем реальную подписанную ссылку
        var signedUrl = await _storageService.GenerateUploadSasUrlAsync(videoId);
        
        // Чистый URL (без токена) для сохранения в БД
        var internalStorageUrl = signedUrl.Split('?')[0];
    
        videoMetadata.VideoId = videoId;
        videoMetadata.OriginalUrl = internalStorageUrl;
        videoMetadata.Status = "Reserved";
        videoMetadata.CreatedAt = DateTime.UtcNow;

        await _videoMetadaRepository.CreateAsync(videoMetadata);

        return signedUrl;
    }
}
