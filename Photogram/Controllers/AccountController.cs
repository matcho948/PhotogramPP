using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IPhotogramRepo _repo;

        public AccountController(IPhotogramRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("/AddNewUser")]
        public async Task<ActionResult> AddNewUser([FromBody] Users user)
        {
            try
            {
                if (user == null || _repo.CheckIfUserExistInDatabase(user))
                    return BadRequest();
                await _repo.AddNewUser(user);
                return CreatedAtRoute(nameof(UserController.GetUserById), new { user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(string username, string password)
        {
            try
            {
                var token = _repo.GenerateToken(username, password);
                if (token == null)
                    return BadRequest("Invalid username or password");
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
