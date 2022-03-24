using Photogram.Models;

namespace Photogram.Data
{
    public interface IPhotogramRepo
    {
        public IEnumerable<Users> GetAllUsers();
    }
}
