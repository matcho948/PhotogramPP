using Microsoft.EntityFrameworkCore;
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

        public async Task AddNewUser(Users user)
        {
          if(user == null)
                throw new ArgumentNullException(nameof(Users));
          await _context.Users.AddAsync(user);
          _context.SaveChanges();
        }

        public bool CheckIfUserExistInDatabase(Users user)
        {
            var searchedUser =  _context.Users.Where(p => p.Name == user.Name || p.Email == user.Email).FirstOrDefault();
            if (searchedUser == null)
              return false;
            return true;
        }

        public Users CheckLoginData(string name, string password)
        {
          var user = _context.Users.FirstOrDefault(p => p.Name == name && p.Password == password);
          return user;
        }

        public async Task DeleteUser(int id)
        {
            var user =  _context.Users.FirstOrDefault(p => p.Id == id);
            if (user != null)
            {
               _context.Users.Remove(user);
               await _context.SaveChangesAsync();
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

        public IEnumerable<Photos> GetAllPhotos()
        {
            return _context.Photos.ToList();
        }
        public Users GetUserWithPhotosById(int id)
        {
            return _context.Users.AsNoTracking().Include(p => p.Photos).FirstOrDefault(p => p.Id == id);
        }
       
    }
}
