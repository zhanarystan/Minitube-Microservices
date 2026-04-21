using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minitube.Upload.Application.DTOs;
public class VideoMetadataCreateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public long FileSize { get; set; }
}
