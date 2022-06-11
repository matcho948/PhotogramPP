using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IPhotosRepo _repo;
        public ReactionController(IPhotosRepo repo)
        {
            _repo = repo;
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
