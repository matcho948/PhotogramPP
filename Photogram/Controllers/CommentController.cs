using Microsoft.AspNetCore.Mvc;
using Photogram.Data;
using Photogram.Models;

namespace Photogram.Controllers
{
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IPhotosRepo _repo;
        private readonly IPhotogramRepo _photogramRepo;
        public CommentController(IPhotosRepo repo, IPhotogramRepo photogramRepo)
        {
            _repo = repo;
            _photogramRepo = photogramRepo; 
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
            var user = await _repo.FindUserByPhotoId(photoId);
            var photo = _photogramRepo.getPhotoById(photoId);
            var notification = new Notifications(user, photo, $"{comment.UserName} commented your photo!");
            await _photogramRepo.AddNotification(notification);
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
