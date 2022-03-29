using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    public class UserController :ControllerBase
    {
        private readonly IPhotogramRepo _repo;
        public UserController(IPhotogramRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("/GetAllUsers")]
        public ActionResult<IEnumerable<Users>> GetAllUsers()
        {
            var users = _repo.GetAllUsers();
            if (users == null)
                return NotFound();
            return Ok(users);
        }
        [HttpGet("/GetUserById/{id}", Name = "GetUserById")]
        public ActionResult<Users> GetUserById(int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpPost("/AddNewUser")]
        public ActionResult AddNewUser([FromBody]Users user)
        {
            if (user == null)
                return NoContent();
            _repo.AddNewUser(user);
            return CreatedAtRoute(nameof(GetUserById), new { Id = user.Id }, user);
        }
        [HttpDelete("/DeleteUser/{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null)
                return NotFound();
            _repo.DeleteUser(id);
            return Ok();
        }
    }
}
