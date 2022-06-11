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

        public async Task<List<Comments>> GetAllCommentsByPhotoId(int photoId)
        {
            var photo = await _context.Photos.Where(p => p.Id == photoId).Include(p => p.Comments).FirstOrDefaultAsync();
            var comments = photo.Comments.ToList();
            return comments;
        }

    }
}
