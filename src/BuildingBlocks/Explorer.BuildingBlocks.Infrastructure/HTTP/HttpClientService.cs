using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Infrastructure.HTTP
{
    public class HttpClientService : IHttpClientService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public HttpClientService() { }

        public string BuildUri(Protocol protocol, string host, int port, string path)
        {
            string scheme = protocol == Protocol.HTTP ? "http" : "https";

            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Host cannot be null or empty.", nameof(host));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            }

            string portString = port != 80 && protocol == Protocol.HTTP || port != 443 && protocol == Protocol.HTTPS ? $":{port}" : "";

            string uri = $"{scheme}://{host}{portString}/{path}";

            return uri;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
        {
            var response = await _httpClient.PostAsync(uri, content);
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, HttpContent content)
        {
            var response = await _httpClient.PutAsync(uri, content);
            return response;
        }

        public async Task<HttpResponseMessage> PatchAsync(string uri, HttpContent content)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
            {
                Content = content
            };
            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            return response;
        }
    }
}
