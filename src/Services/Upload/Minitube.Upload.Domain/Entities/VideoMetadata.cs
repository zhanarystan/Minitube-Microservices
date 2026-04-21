using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minitube.Upload.Domain.Entities;

public class VideoMetadata
{
    public Guid VideoId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid AuthorId { get; set; }
    public string OriginalUrl { get; set; }
    public long FileSize { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
