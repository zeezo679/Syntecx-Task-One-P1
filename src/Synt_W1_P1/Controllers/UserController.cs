using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synt_W1_P1.Models;
using Synt_W1_P1.Services.UserService;
using System.Threading.Tasks;

namespace Synt_W1_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) { 
            _userService = userService;
        }


        //Read
        [HttpGet("users")]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();   

            return Ok(users);
        }

        [HttpGet("get_user/{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user is null)
                return NotFound("User is not found");

            return Ok(user);
        }

        [HttpPost("add_user")]
        public async Task<ActionResult> AddUser([FromBody] User user)
        {
            var createdUser = await _userService.AddUser(user);

            if (createdUser is null)
                return Conflict("User with this email already exists or invalid data");  //409 status code

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);

        }

        [HttpPut("edit_user/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var updatedUser = await _userService.UpdateUser(id, user);

            if (updatedUser is null)
                return Conflict("User with this email already exists or invalid data");

            return Ok(updatedUser);
        }

        [HttpDelete("delete_user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUser(id);

            if(!deleted)
                return NotFound("User may be not found");

            return NoContent();
        }
    }
}
