using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minitube.Upload.Domain.Entities;
using Minitube.Upload.Infrastructure.Models;

namespace Minitube.Upload.Infrastructure.Mappers;
public static class VideoMetadaMapper
{
    public static DbVideoMetadata ToAzureSqlDb(this VideoMetadata videoMetadata)
    {
        return new DbVideoMetadata
        {
            VideoId = videoMetadata.VideoId,
            Title = videoMetadata.Title,
            Description = videoMetadata.Description,
            AuthorId = videoMetadata.AuthorId,
            FileSize = videoMetadata.FileSize,
            OriginalUrl = videoMetadata.OriginalUrl,
            Status = videoMetadata.Status,
            CreatedAt = videoMetadata.CreatedAt
        };
    }

    public static VideoMetadata ToDomain(this DbVideoMetadata dbVideoMetadata)
    {
        return new VideoMetadata
        {
            VideoId = dbVideoMetadata.VideoId,
            Title = dbVideoMetadata.Title,
            Description = dbVideoMetadata.Description,
            AuthorId = dbVideoMetadata.AuthorId,
            FileSize = dbVideoMetadata.FileSize,
            OriginalUrl = dbVideoMetadata.OriginalUrl,
            Status = dbVideoMetadata.Status,
            CreatedAt = dbVideoMetadata.CreatedAt
        };
    }
}
