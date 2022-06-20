using Photogram.Models;

namespace Photogram.Data
{
    public interface IPhotogramRepo
    {
        public IEnumerable<Users> GetAllUsers();
        public Users GetUserById(int id);
        public List<Users> GetUsersByName(string name);
        public Task DeleteUser(int id);
        public Task AddNewUser(Users user);
        public bool CheckIfUserExistInDatabase(Users user);
        public Users CheckLoginData(string name, string password);
        public IEnumerable<Photos> GetAllPhotos();
        public Users GetUserWithPhotosById(int id);
        public string? GenerateToken(string name, string password);
        public Photos getPhotoById(int id);
        public Task addNewPhoto(int userId, Photos photo);
        public Task setProfilePhoto(Photos photo);
        public Task deleteProfilePhoto(Photos photo);
        public Photos getPhotoToDeleteById(int id);
        public Task deletePhoto(Photos photo);
        public Task addFollower(Users user, Followers followerId);
        public Task<List<Users>> GetFollowersList(int userId);
        public Task<List<Users>> GetFolloweredUsers(int userId);
        public Users getUserByPhotoId(int photoId);
        public Task ChangeUserName(int userId, string username);
        public Task ChangePassword(int userId, string password);
        public Task ChangeEmail(int userId, string email);
        public Task AddNotification(Notifications notification);
        public Task<List<Notifications>> GetAllNotificationForUser(int userId);
        public int getNumberOfUsers();
        public int getNumberOfPhotos();
        public List<Users> SearchUsers(string username);
    }
}
