using Photogram.Models;

namespace Photogram.Data
{
    public interface IPhotogramRepo
    {
        public IEnumerable<Users> GetAllUsers();
        public Users GetUserById(int id);
        public void DeleteUser(int id);
        public void AddNewUser(Users user);
    }
}
