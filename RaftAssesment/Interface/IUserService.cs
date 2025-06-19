using RaftAssesment.Models;

namespace RaftAssesment.Interface
{
    public interface IUserService
    {
        public Task<ApiResponse<Users>> GetUsersPaginated(long pageNo);
        public Task<ApiResponse<User>> GetUserById(long userId);
    }
}
