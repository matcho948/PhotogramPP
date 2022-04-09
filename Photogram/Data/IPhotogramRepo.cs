using Photogram.Models;

namespace Photogram.Data
{
    public interface IPhotogramRepo
    {
        public IEnumerable<Users> GetAllUsers();
        public Users GetUserById(int id);
        public Task DeleteUser(int id);
        public Task AddNewUser(Users user);
        public bool CheckIfUserExistInDatabase(Users user);
        public Users CheckLoginData(string name, string password);
    }
}
