using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minitube.Shared.Contracts.MessageBusEvents;
using Minitube.Upload.Application.DTOs;
using Minitube.Upload.Domain.Entities;

namespace Minitube.Upload.Application.Mappers;
public static class VideoMetadaMapper
{
    public static VideoMetadata ToDomain(this VideoMetadataCreateDto dto, Guid authorId)
    {
        return new VideoMetadata
        {
            VideoId = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            AuthorId = authorId,
            FileSize = dto.FileSize,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static VideoReservedEvent ToEvent(this VideoMetadata videoMetadata)
    {
        return new VideoReservedEvent
        {
            Id = videoMetadata.VideoId,
            Title = videoMetadata.Title,
            Description = videoMetadata.Description,
            AuthorId = videoMetadata.AuthorId,
            FileSize = videoMetadata.FileSize,
            BlobPath = videoMetadata.OriginalUrl,
            Status = videoMetadata.Status,
            CreatedAt = videoMetadata.CreatedAt
        };
    }
}


