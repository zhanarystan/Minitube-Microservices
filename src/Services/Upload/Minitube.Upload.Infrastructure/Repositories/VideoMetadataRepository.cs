using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Minitube.Upload.Application.Interfaces;
using Minitube.Upload.Domain.Entities;
using Minitube.Upload.Infrastructure.Data;
using Minitube.Upload.Infrastructure.Mappers;
using Minitube.Upload.Infrastructure.Models;

namespace Minitube.Upload.Infrastructure.Repositories;

public class VideoMetadataRepository : IVideoMetadataRepository
{

        private readonly DapperContext _context;
        private readonly string _table;

    public VideoMetadataRepository(DapperContext context, string table)
    {
        _table = table;
        _context = context;
    }
    
    public async Task CreateAsync(VideoMetadata videoMetadata)
    {
        var dbVideoMetada = videoMetadata.ToAzureSqlDb();
        var sql = $@"
            INSERT INTO {_table} 
            (VideoId, Title, Description, AuthorId, OriginalUrl, FileSize, Status, CreatedAt)
            VALUES 
            (@VideoId, @Title, @Description, @AuthorId, @OriginalUrl, @FileSize, @Status, @CreatedAt);";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, dbVideoMetada);
    }

    public async Task<VideoMetadata?> GetAsync(Guid videoId)
    {
        var sql = $"SELECT * FROM {_table} WHERE VideoId = @VideoId";
        using var connection = _context.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<DbVideoMetadata>(sql, new { VideoId = videoId });
        return result?.ToDomain();
    }

    public async Task UpdateStatusAsync(Guid videoId, string status)
    {
        var sql = $"UPDATE {_table} SET Status = @Status WHERE VideoId = @VideoId";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { VideoId = videoId, Status = status });
    }
}
