using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IPhotosRepo _repo;
        public CommentController(IPhotosRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("/GetAllCommentsForPhoto/{photoId}", Name ="GetComments")]
        public async Task<ActionResult<Comments>> GetAllCommentsForPhoto(int photoId)
        {
            var comments = await _repo.GetAllCommentsByPhotoId(photoId);
            if (comments == null)
                return NotFound();
            else
                return Ok(comments);
        }
        [HttpPost("AddCommentToPhoto/{photoId}")]
        public async Task<ActionResult> AddCommentToPhoto(Comments comment,int photoId)
        {
            await _repo.AddCommentToPhoto(comment, photoId);
            return Ok();
        }
        [HttpDelete("/DeleteComment/{photoId}/{commentId}")]
        public async Task<ActionResult> DeleteComment(int photoId,int commentId)
        {
            await _repo.DeleteCommentById(photoId,commentId);
            return Ok();
        }

    }
}
