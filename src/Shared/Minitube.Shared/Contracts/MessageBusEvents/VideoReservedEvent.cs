namespace Minitube.Shared.Contracts.MessageBusEvents;
public class VideoReservedEvent
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid AuthorId { get; init; }
    
    public long FileSize { get; init; }
    
    public string BlobPath { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    
    public DateTime CreatedAt { get; init; }
}
