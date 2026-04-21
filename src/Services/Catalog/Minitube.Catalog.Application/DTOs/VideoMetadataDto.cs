using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minitube.Catalog.Application.DTOs;
/// <summary>
/// DTO для передачи метаданных видео между фронтендом и API.
/// </summary>
public record VideoMetadataDto(
    Guid Id,
    string Title,
    string BlobUrl,
    string? Description,
    string? PreviewUrl,
    DateTime PublishTime,
    string Status
);