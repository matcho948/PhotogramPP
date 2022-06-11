using Photogram.Models;

namespace Photogram.Data
{
    public interface IPhotosRepo
    {
        public Task<List<Comments>> GetAllCommentsByPhotoId(int photoId);
        public Task AddCommentToPhoto(Comments comment, int photoId);
        public Task DeleteCommentById(int photoId, int commentId);
    }
}
