using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IPhotogramRepo _repo;
        private readonly IPhotosRepo _photoRepo;
        public FollowersController(IPhotogramRepo repo, IPhotosRepo photoRepo)
        {
            _repo = repo;
            _photoRepo = photoRepo;
        }
        //user id id usera, którego ktoś chce obserwować
        // follower id user, który kogoś zaobserwował
        [HttpPost("/AddFollowers")]
        public async Task<ActionResult> AddFollowers(int userId, Followers followerId)
        {
            var user = _repo.GetUserById(userId);
            var follower = _repo.GetUserById(followerId.UserId);
            if (user == null || followerId == null)
            {
                return NotFound();
            }
            var notification = new Notifications(user, null, $"{follower.Name} follow your profile!");
            await _repo.AddNotification(notification);
            await _repo.addFollower(user, followerId);
            return Ok();
        }
        [HttpGet("/GetFollowersList/{userId}")]
        public async Task<ActionResult<List<Users>>> GetFollowersList(int userId)
        {
            if (userId == null)
                return NotFound();
            var followers = await  _repo.GetFollowersList(userId);
            return Ok(followers);
        }
        [HttpGet("/GetAmountOfFollowers/{userId}")]
        public async Task<ActionResult<int>> GetAmountOfFollowers(int userId)
        {
            if (userId == null)
                return NotFound();
            var amount = await _photoRepo.CountFollowers(userId);
            return Ok(amount);
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
