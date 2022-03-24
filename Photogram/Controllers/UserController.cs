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
        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetAllUsers()
        {
            var users = _repo.GetAllUsers();
            if (users == null)
                return NotFound();
            return Ok();
        }
    }
}
