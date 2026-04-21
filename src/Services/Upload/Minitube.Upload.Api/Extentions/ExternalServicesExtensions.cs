using System.IdentityModel.Tokens.Jwt;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace Minitube.Upload.Api.Extentions;

public static class ExternalServicesExtensions
{
    public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApi(options =>
                        {
                            configuration.Bind("AzureAd", options);
                
                            options.TokenValidationParameters.ValidateIssuer = false;
                        }, options => { configuration.Bind("AzureAd", options); });

        services.AddSingleton(x => 
            new BlobServiceClient(
                new Uri(configuration["AzureStorage:BlobServiceUri"]), 
                new DefaultAzureCredential()));
    }    
}
