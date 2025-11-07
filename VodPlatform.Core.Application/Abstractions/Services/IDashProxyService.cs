using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Application.Abstractions.Services
{
    public interface IDashProxyService
    {
        Task<string> GetMpdFileUrl(string relativePath);
        string GetContentType(string filePath);
    }
}
