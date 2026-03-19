using Minitube.Upload.Domain.Constants;

namespace Minitube.Upload.Domain.Entities;

public class Video
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = VideoUploadStatus.Pending; // Pending, Uploaded, Processing, Ready
    public string BlobPath { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}