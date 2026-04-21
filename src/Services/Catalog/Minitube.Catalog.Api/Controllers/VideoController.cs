using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minitube.Catalog.Application.DTOs;

namespace Minitube.Catalog.Api.Controllers;

// [Authorize] // Только для авторизованных пользователей Microsoft Entra
[ApiController]
[Route("api/v1/[controller]")]
public class VideoController : ControllerBase
{
    private readonly ILogger<VideoController> _logger;

    public VideoController(ILogger<VideoController> logger)
    {
        _logger = logger;
    }

    [HttpGet("own")]
    public ActionResult<IEnumerable<VideoMetadataDto>> ListOwnVideos()
    {
        // В реальности здесь будет поиск в БД по AuthorId (oid из токена)
        // var userId = User.FindFirst("oid")?.Value;
        
        var userName = User.FindFirst("name")?.Value ?? "User";

        var mockVideos = new List<VideoMetadataDto>
        {
            new VideoMetadataDto(
                Guid.NewGuid(),
                $"{userName}: Как я деплоил микросервисы в Azure",
                "https://storage.minitube.com/v1.mp4",
                "Разбор архитектуры Minitube",
                "https://images.unsplash.com/photo-1460925895917-afdab827c52f?w=400",
                DateTime.UtcNow.AddHours(-2),
                "published"
            ),
            new VideoMetadataDto(
                Guid.NewGuid(),
                $"{userName}: Разбор Garbage Collector в .NET 10",
                "https://storage.minitube.com/v2.mp4",
                "Глубокое погружение в Pinned Object Heap",
                "https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=400",
                DateTime.UtcNow.AddMinutes(-15),
                "processing"
            ),
            new VideoMetadataDto(
                Guid.NewGuid(),
                $"{userName}: Ошибка в Consistent Hashing",
                "https://storage.minitube.com/v3.mp4",
                "Это видео упало при обработке",
                null,
                DateTime.UtcNow.AddDays(-1),
                "error"
            )
        };

        return Ok(mockVideos);
    }
}
