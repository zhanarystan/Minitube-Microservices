using Minitube.Upload.Application.Interfaces;
using Minitube.Upload.Application.Services;
using Minitube.Upload.Infrastructure.Data;
using Minitube.Upload.Infrastructure.Repositories;
using Minitube.Upload.Infrastructure.Services;

namespace Minitube.Upload.Api.Extentions;
public static class AppExtensions
{
    public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConnectionString = configuration.GetConnectionString("AzureSql");
                
        services.AddSingleton<DapperContext>(provider => new DapperContext(sqlConnectionString));

        services.AddScoped<IVideoMetadataRepository>(provider => 
            new VideoMetadataRepository(
                provider.GetRequiredService<DapperContext>(), 
                "upload.VideoMetadata"));

        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IVideoStorageService, VideoStorageService>();
        services.AddScoped<IMessageBus, MassTransitBus>();
    }
}
