using Photogram.Models;
using Microsoft.EntityFrameworkCore;

namespace Photogram.Data
{
    public class PhotosRepo : IPhotosRepo
    {
        private readonly PhotogramDbContext _context;
        public PhotosRepo(PhotogramDbContext context)
        {
            _context = context;
        }

        public async Task AddCommentToPhoto(Comments comment, int photoId)
        {
            if(comment == null || photoId == null)
                throw new ArgumentNullException(nameof(comment));
            var photo = await _context.Photos.Where(p => p.Id == photoId).Include(p => p.Comments).FirstOrDefaultAsync();
            if (photo != null)
            {
                photo.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddReactionToPhoto(Reactions reaction, int photoId)
        {
            if (reaction == null || photoId == null)
                throw new ArgumentNullException(nameof(reaction));
            var photo = await _context.Photos.Where(p => p.Id == photoId).Include(p => p.Reactions).FirstOrDefaultAsync();
            if (photo != null)
            {
                photo.Reactions.Add(reaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountFollowers(int userId)
        {
            var user = await _context.Users.Include(p=>p.Followers).FirstOrDefaultAsync(p=>p.Id == userId);
            if (user != null)
            {
                return user.Followers.Count();
            }
            return 0;
        }

        public async Task DeleteCommentById(int photoId, int commentId)
        {
            var photo = await _context.Photos.Where(p => p.Id == photoId).Include(p => p.Comments).FirstOrDefaultAsync();
            if (photo != null)
            {
                var comment = photo.Comments.Where(p => p.Id == commentId).FirstOrDefault();
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteReactionById(int photoId, int reactionId)
        {
            var photo = await _context.Photos.Where(p => p.Id == photoId).Include(p => p.Reactions).FirstOrDefaultAsync();
            if (photo != null)
            {
                var reaction = photo.Reactions.Where(p => p.Id == reactionId).FirstOrDefault();
                if (reaction != null)
                {
                    _context.Reactions.Remove(reaction);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<Users> FindUserByPhotoId(int photoId)
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();
            foreach(var user in users)
            {
                if (user.Photos.Any(p => p.Id == photoId))
                {
                    return(await _context.Users.FirstOrDefaultAsync(p => p.Id == user.Id));
                }
            }
            return null;
        }

        public async Task<List<Comments>> GetAllCommentsByPhotoId(int photoId)
        {
            var photo = await _context.Photos.Where(p => p.Id == photoId).Include(p => p.Comments).FirstOrDefaultAsync();
            if (photo != null)
            {
                var comments = photo.Comments.ToList();
                return comments;
            }
            return null;
        }

        public async Task<List<Reactions>> GetAllReactionsByPhotoId(int photoId)
        {
            var photo = await _context.Photos.Where(p => p.Id == photoId).Include(p => p.Reactions).FirstOrDefaultAsync();
            if (photo != null)
            {
                var reactions = photo.Reactions.ToList();
                return reactions;
            }
            return null;

        }
    }
}
