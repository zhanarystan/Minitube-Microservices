using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minitube.Upload.Application.Interfaces;
public interface IVideoStorageService
{   
    Task<string> GenerateUploadSasUrlAsync(Guid videoId); 
}
