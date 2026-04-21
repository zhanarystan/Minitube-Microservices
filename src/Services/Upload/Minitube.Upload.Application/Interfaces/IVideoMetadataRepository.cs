using System;
using System.Threading.Tasks;
using Minitube.Upload.Domain.Entities;

namespace Minitube.Upload.Application.Interfaces;

public interface IVideoMetadataRepository
{
    Task CreateAsync(VideoMetadata videoMetadata);
    Task<VideoMetadata?> GetAsync(Guid videoId);
    Task UpdateStatusAsync(Guid videoId, string status);    
}
