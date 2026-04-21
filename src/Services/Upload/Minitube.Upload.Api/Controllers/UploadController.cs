using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minitube.Upload.Application.DTOs;
using Minitube.Upload.Application.Interfaces;
using Minitube.Upload.Application.Mappers;

namespace Minitube.Upload.Api.Controllers;

[Authorize] // Теперь анонимный запрос получит 401 Unauthorized
[ApiController]
[Route("api/v1/[controller]")]
public class UploadController : ControllerBase
{
    private readonly IVideoService _videoService;

    public UploadController(IVideoService videoService)
    {
        _videoService = videoService;
    }

    [HttpPost("reserve")]
    public async Task<IActionResult> Reserve([FromBody] VideoMetadataCreateDto videoMetadata)
    {
        var authorId = GetCurrentUserId();

        if (authorId == Guid.Empty)
        {
            return Unauthorized("User ID not found in token.");
        }
        
        return Ok(await _videoService.Reserve(videoMetadata.ToDomain(authorId)));
    }

    /// <summary>
    /// Extracts user ID from Claims (JWT token)
    /// </summary>
    private Guid GetCurrentUserId()
    {
        // Пытаемся найти OID (он всегда в формате GUID)
        var userIdClaim = User.FindFirst("oid")?.Value 
                        ?? User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

        if (Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        throw new Exception("User ID claim is missing or invalid.");
    }
}
