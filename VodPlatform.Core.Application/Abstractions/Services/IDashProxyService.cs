using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Application.Abstractions.Services
{
    public interface IDashProxyService
    {
        Task<Stream> GetRemoteFileAsync(string relativePath, CancellationToken cancellationToken = default);
        string GetContentType(string filePath);
    }
}
