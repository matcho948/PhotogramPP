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

        public void AddNewUser(Users user)
        {
          if(user == null)
                throw new ArgumentNullException(nameof(Users));
          _context.Users.Add(user);
          _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(p => p.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Users GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(p => p.Id == id);
        }

    }
}
