using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VodPlatform.Core.Application.Abstractions.Services;

namespace VodPlatform.Infrastructure.VideoStreaming.Services
{
    public class DashProxyService : IDashProxyService
    {
        private readonly string _nginxBaseUrl;

        public DashProxyService(IConfiguration configuration)
        {
            _nginxBaseUrl = configuration["DashStreaming:NginxBaseUrl"]
                            ?? throw new ArgumentNullException("DashStreaming:NginxBaseUrl is missing in configuration.");
        }

        // Zwraca link do pliku .mpd
        public async Task<string> GetMpdFileUrl(string relativePath)
        {
            var decodedPath = Uri.UnescapeDataString(relativePath);
            return $"{_nginxBaseUrl}{decodedPath}";
        }

        // Pozostaje przydatne do typu MIME
        public string GetContentType(string filePath)
        {
            var ext = Path.GetExtension(filePath).ToLowerInvariant();

            return ext switch
            {
                ".mpd" => "application/dash+xml",
                ".m4s" => "video/iso.segment",
                ".mp4" => "video/mp4",
                ".webm" => "video/webm",
                _ => "application/octet-stream"
            };
        }
    }
}