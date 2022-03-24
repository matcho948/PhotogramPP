using Photogram.Models;

namespace Photogram.Data
{
    public class PhotogramRepo : IPhotogramRepo
    {
        private readonly PhotogramDbContext _context;
        public PhotogramRepo(PhotogramDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
