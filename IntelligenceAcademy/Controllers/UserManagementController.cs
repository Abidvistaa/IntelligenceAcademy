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

        [Authorize]
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var borrows = await _userService.GetAllAsync();
            return Ok(borrows);
        }

        [Authorize]
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var borrow = await _userService.GetByIdAsync(id);
            return Ok(borrow);
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User model)
        {
            await _userService.AddAsync(model);

            return Ok(new { Success = true, Message = "User added successfully." });
        }

        [Authorize]
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User model)
        {
            await _userService.UpdateAsync(id, model);

            return Ok(new { Success = true, Message = "User updated successfully" });
        }

        [Authorize]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);

            return Ok(new { Success = true, Message = $"User with ID {id} deleted successfully." });
        }
    }
}
