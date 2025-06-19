using RaftAssesment.Models;

namespace RaftAssesment
{
    public interface IHttpClientService
    {
        public Task<ApiResponse<string>> SendHTTPGetWithToken(object reqObj, string url, string apiKey);
    }
}
