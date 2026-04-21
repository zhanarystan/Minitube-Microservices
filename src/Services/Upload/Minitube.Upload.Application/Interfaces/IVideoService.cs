using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minitube.Upload.Domain.Entities;

namespace Minitube.Upload.Application.Interfaces;

public interface IVideoService
{
    Task<string> Reserve(VideoMetadata videoMetadata);
}
