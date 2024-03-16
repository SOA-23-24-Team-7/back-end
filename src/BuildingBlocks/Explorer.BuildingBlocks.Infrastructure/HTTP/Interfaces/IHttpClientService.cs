using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces
{
    public interface IHttpClientService
    {
        string BuildUri(Protocol protocol, string host, int port, string path);
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);
        Task<HttpResponseMessage> PutAsync(string uri, HttpContent content);
        Task<HttpResponseMessage> PatchAsync(string uri, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(string uri);
    }

    public enum Protocol
    {
        HTTP,
        HTTPS
    }
}
