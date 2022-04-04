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

    }
}
