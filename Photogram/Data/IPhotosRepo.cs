using Photogram.Models;

namespace Photogram.Data
{
    public interface IPhotosRepo
    {
        public Task<List<Comments>> GetAllCommentsByPhotoId(int photoId);
        public Task<List<Reactions>> GetAllReactionsByPhotoId(int photoId);
        public Task AddCommentToPhoto(Comments comment, int photoId);
        public Task AddReactionToPhoto(Reactions reaction, int photoId);
        public Task DeleteCommentById(int photoId, int commentId);
        public Task DeleteReactionById(int photoId, int reactionId);
        public Task<int> CountFollowers(int userId);
        public Task<Users> FindUserByPhotoId(int photoId);
    }
}
