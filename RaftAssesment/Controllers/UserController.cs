using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaftAssesment.Interface;
using RaftAssesment.Models;

namespace RaftAssesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("~/api/[controller]/get-users-paginated")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUsers([FromQuery] long page)
        {
            ApiResponse<Users> apiResponse = new ApiResponse<Users>();
            try
            {
                apiResponse = await _userService.GetUsersPaginated(page);
                return StatusCode(200, apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(400,apiResponse);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserById(int id)
        {
            ApiResponse<User> apiResponse = new ApiResponse<User>();
            try
            {
                apiResponse = await _userService.GetUserById(id);
                if (!apiResponse.Success)
                {
                    return NotFound(apiResponse);
                }
                return StatusCode(200, apiResponse);
            }
            catch (Exception ex)
            {
                return NotFound(apiResponse);
            }
        }
    }
}
