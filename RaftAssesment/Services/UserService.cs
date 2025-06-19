using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RaftAssesment.Interface;
using RaftAssesment.Models;
using System.Security.AccessControl;

namespace RaftAssesment.Services
{
    public class UserService : IUserService
    {
        private readonly string URL;
        private readonly string apiKey;
        private readonly IHttpClientService _httpClient;
        private readonly IMemoryCache _cache;
        private string cacheKey = cacheKeys.UserCachePaginater;
        private long cacheTimeSpan = cacheKeys.UserCachePaginaterSpan;
        public UserService(IConfiguration configuration, IHttpClientService httpClient, IMemoryCache cache) 
        {
            URL = configuration.GetSection("AppSettings:URL").Value;
            apiKey = configuration.GetSection("AppSettings:API_KEY").Value;
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<ApiResponse<Users>> GetUsersPaginated(long pageNo)
        {
            ApiResponse<Users> apiResponse = new ApiResponse<Users>();
            ApiResponse<string> httpResponse = new ApiResponse<string>();
            Users cacheResponse = new Users();
            try
            {
                string userUrl = URL + "users?page=" + pageNo;
                httpResponse = await _httpClient.SendHTTPGetWithToken(new User(), userUrl, apiKey);
                apiResponse.ResponseObject = JsonConvert.DeserializeObject<Users>(httpResponse.ResponseObject);

                //set cache
                cacheResponse = apiResponse.ResponseObject;
                InMemoryCaching imCache = new InMemoryCaching(_cache);
                imCache.setCache(cacheKey, cacheResponse, cacheTimeSpan);

            }
            catch (Exception ex)
            {
                apiResponse.SetErrorCode();
            }
            return apiResponse;
        }

        public async Task<ApiResponse<User>> GetUserById(long userId)
        {
            ApiResponse<User> apiResponse = new ApiResponse<User>();
            ApiResponse<string> httpResponse = new ApiResponse<string>();
            Users cacheResponse = new Users();
            User userObj = new User();
            try
            {
                if(!_cache.TryGetValue(cacheKey, out cacheResponse))
                {
                    string userUrl = URL + "users/" + userId;
                    httpResponse = await _httpClient.SendHTTPGetWithToken(new User(), userUrl, apiKey);
                    apiResponse.ResponseObject = JsonConvert.DeserializeObject<User>(httpResponse.ResponseObject);
                }
                else
                {
                    var user = cacheResponse.data.FirstOrDefault(user => user.id == userId);
                    if (user != null)
                    {
                        userObj.data = user;
                        userObj.support = cacheResponse.support;
                        apiResponse.ResponseObject = userObj;
                    }
                    else
                    {
                        string userUrl = URL + "users/" + userId;
                        httpResponse = await _httpClient.SendHTTPGetWithToken(new User(), userUrl, apiKey);
                        apiResponse.ResponseObject = JsonConvert.DeserializeObject<User>(httpResponse.ResponseObject);
                    }
                }
                return apiResponse;
            }
            catch(Exception ex)
            {
                apiResponse.SetErrorCode();
            }
            return apiResponse;
        }
    }
}
