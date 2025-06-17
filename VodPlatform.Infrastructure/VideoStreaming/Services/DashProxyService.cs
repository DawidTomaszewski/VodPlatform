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
        private readonly HttpClient _httpClient;
        private readonly string _nginxBaseUrl;

        public DashProxyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _nginxBaseUrl = configuration["DashStreaming:NginxBaseUrl"]
                            ?? throw new ArgumentNullException("DashStreaming:NginxBaseUrl is missing in configuration.");
        }

        public async Task<Stream> GetRemoteFileAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            var decodedPath = Uri.UnescapeDataString(relativePath);
            var fullUrl = $"{_nginxBaseUrl}{decodedPath}";

            var response = await _httpClient.GetAsync(fullUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new FileNotFoundException($"File not found: {fullUrl}");

            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }

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
