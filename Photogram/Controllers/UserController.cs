using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    //[Authorize]
    public class UserController :ControllerBase
    {
        private readonly IPhotogramRepo _repo;
        public UserController(IPhotogramRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("/GetAllUsers")]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            var users = _repo.GetAllUsers();
            if (users == null)
                return NotFound();
            return Ok(users);
        }
        [HttpGet("/GetUserById/{id}", Name = "GetUserById")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
       
        // After password hashing, this endpoint not works properly
        //[HttpGet("/CheckLoginData/{name}/{password}")]
        //public async Task<ActionResult<Users>> CheckLoginData(string name,string password)
        //{
        //    var user = _repo.CheckLoginData(name, password);
        //    if (user == null)
        //        return NotFound();
        //    return Ok(user);
        //}

        [HttpDelete("/DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null)
                return NotFound();
            await _repo.DeleteUser(id);
            return Ok();
        }
    }
}
