using Newtonsoft.Json;
using RaftAssesment.Models;
using System.Text;

namespace RaftAssesment.Services
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClientService() { }

        public async Task<ApiResponse<string>> SendHTTPGetWithToken(object reqObj, string url, string apiKey)
        {
            ApiResponse<string> res = new ApiResponse<string>();
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(reqObj,Newtonsoft.Json.Formatting.Indented);

                var httpContent = new StringContent(jsonRequest, Encoding.UTF8,"application/json");

                httpContent.Headers.Remove("Content-Type");
                httpContent.Headers.Add("Content-Type", "application/json");
                var postURI = new Uri(url);

                using(HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                    var response = await client.GetAsync(postURI);

                    //res.StatusCode = response.StatusCode.ToString();
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var respStr = await response.Content.ReadAsStringAsync();
                        res.ResponseObject = respStr;
                        res.Success = true;
                    }
                    else
                    {
                        res.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
            }
            return res;
        }
    }
}
