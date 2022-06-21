using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IPhotosRepo _repo;
        private readonly IPhotogramRepo _photogramRepo;
        public ReactionController(IPhotosRepo repo, IPhotogramRepo photogramRepo)
        {
            _repo = repo;
            _photogramRepo = photogramRepo; 
        }

        [HttpGet("/GetAllReactionsForPhoto/{photoId}", Name = "GetReactions")]
        public async Task<ActionResult<List<Reactions>>> GetAllReactionsForPhoto(int photoId)
        {
            var reactions = await _repo.GetAllReactionsByPhotoId(photoId);
            if (reactions == null)
                return NotFound();
            return Ok(reactions);
        }
        [HttpPost("AddReactionToPhoto/{photoId}")]
        public async Task<ActionResult> AddReactionToPhoto(Reactions reaction, int photoId)
        {
            var user = await _repo.FindUserByPhotoId(photoId);
            var photo = _photogramRepo.getPhotoById(photoId);
            var user2 = _photogramRepo.GetUserById(reaction.userId);
            var notification = new Notifications(user,photo,$"{user2.Name} reacted to your photo!");
            await _photogramRepo.AddNotification(notification);
            await _repo.AddReactionToPhoto(reaction, photoId);
            return Ok();
        }
        [HttpDelete("/DeleteReaction/{photoId}/{reactionId}")]
        public async Task<ActionResult> DeleteReaction(int photoId, int reactionId)
        {
            await _repo.DeleteReactionById(photoId, reactionId);
            return Ok();
        }
    }
}
