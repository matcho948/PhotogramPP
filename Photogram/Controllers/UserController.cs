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
        [HttpGet("/GetUserByPhotoId/{photoId}")]
        public async Task<ActionResult<Users>> getUserByPhotoId(int photoId)
        {
            var user = _repo.getUserByPhotoId(photoId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpDelete("/DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null)
                return NotFound();
            await _repo.DeleteUser(id);
            return Ok();
        }

        [HttpPatch("/ChangeUserName/{id}")]
        public async Task<ActionResult> ChangeUserName(int id, string userName)
        {
            if(!_repo.CheckIfUserExistInDatabase(_repo.GetUserById(id)))
                return NotFound();
            await _repo.ChangeUserName(id, userName);
            return Ok();
        }

        [HttpPatch("/ChangeEmail/{id}")]
        public async Task<ActionResult> ChangeEmail(int id, string email)
        {
            if (!_repo.CheckIfUserExistInDatabase(_repo.GetUserById(id)))
                return NotFound();
            try
            {
                await _repo.ChangeEmail(id, email);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPatch("/ChangePassword/{id}")]
        public async Task<ActionResult> ChangePassword(int id, string password)
        {
            if (!_repo.CheckIfUserExistInDatabase(_repo.GetUserById(id)))
                return NotFound();
            try
            {
                await _repo.ChangePassword(id, password);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpGet("/GetRecomendedUsers")]
        public async Task<ActionResult<List<Users>>> getRecomendedUsers()
        {
            var idList = new List<int>();
            var numberOfUsers = _repo.getNumberOfUsers();
            Random random = new Random();
            var users = new List<Users>();
            if (numberOfUsers < 20)
                users = _repo.GetAllUsers().ToList();
            else
            {
                while (users.Count < 20)
                {
                    var id = random.Next(numberOfUsers);
                    if (!idList.Contains(id))
                    {
                        var user = _repo.GetUserById(id);
                        if (user != null)
                            users.Add(user);
                    }
                    idList.Add(id);
                }
            }
            return Ok(users);
        }

    }
}
