using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IPhotogramRepo _repo;
        public FollowersController(IPhotogramRepo repo)
        {
            _repo = repo;
        }
        //user id id usera, którego ktoś chce obserwować
        // follower id user, który kogoś zaobserwował
        [HttpPost("/AddFollowers")]
        public async Task<ActionResult> AddFollowers(int userId, int followerId)
        {
            var user = _repo.GetUserById(userId);
            var follower = _repo.GetUserById(followerId);
            if (user == null || follower == null)
            {
                return NotFound();
            }
            _repo.addFollower(user, follower);
            return Ok();
        }
        [HttpGet("/GetFollowersList/{userId}")]
        public async Task<ActionResult<List<Users>>> GetFollowersList(int userId)
        {
            if (userId == null)
                return NotFound();
            var followers = _repo.GetFollowersList(userId);
            return Ok(followers.Followers);
        }
        [HttpGet("/GetUsersFolloweredByUser/{userId}")]
        public async Task<ActionResult<List<Users>>> GetUsersFolloweredByUser(int userId)
        {
            if (userId == null)
                return NotFound();
            var followers = await _repo.GetFolloweredUsers(userId);
            return Ok(followers);
        }
    }
}
