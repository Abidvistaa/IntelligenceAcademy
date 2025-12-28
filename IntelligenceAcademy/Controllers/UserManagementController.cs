using IntelligenceAcademy.Model;
using IntelligenceAcademy.Service;
using IntelligenceAcademy.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntelligenceAcademy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagementController : ControllerBase
    {
        IUserService _userService;
        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel obj)
        {
            var result = await _userService.LoginAsync(obj);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User model)
        {
            await _userService.AddAsync(model);

            return Ok(new { Success = true, Message = "User added successfully." });
        }

        [AllowAnonymous]
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User model)
        {
            await _userService.UpdateAsync(id, model);

            return Ok(new { Success = true, Message = "User updated successfully" });
        }

        [AllowAnonymous]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);

            return Ok(new { Success = true, Message = $"User with ID {id} deleted successfully." });
        }

        [AllowAnonymous]
        [HttpPost("GoogleSignIn")]
        public async Task<IActionResult> GoogleSignIn([FromBody] string idToken)
        {
            var result = await _userService.GoogleSignInAsync(idToken);

            return Ok(result);
        }
    }
}
