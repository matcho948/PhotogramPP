using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Photogram.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Photogram.Data
{
    public class PhotogramRepo : IPhotogramRepo
    {
        private readonly PhotogramDbContext _context;
        private readonly IPasswordHasher<Users> _hasher;
        private readonly AuthenticationSettings _authentication;

        public PhotogramRepo(PhotogramDbContext context, IPasswordHasher<Users> hasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _hasher = hasher;
            _authentication = authenticationSettings;
        }

        public async Task AddNewUser(Users user)
        {
          if(user == null)
                throw new ArgumentNullException(nameof(Users));

          user.Password = _hasher.HashPassword(user, user.Password);
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

        public string? GenerateToken(string name, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == name);
            if (user == null)
                return null;

            var check = _hasher.VerifyHashedPassword(user, user.Password, password);
            if (check == PasswordVerificationResult.Failed)
                return null;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("isBanned", user.IsBanned.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authentication.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_authentication.DurationInMinutes);

            var token = new JwtSecurityToken(_authentication.JwtIssuer,
               _authentication.JwtIssuer, claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
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
