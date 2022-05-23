
﻿using Microsoft.EntityFrameworkCore;
using Photogram.Models;
﻿using Microsoft.AspNetCore.Identity;
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
                new Claim("ID", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("Role", user.Role.ToString()),
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

        public IEnumerable<Photos> GetAllPhotos()
        {
            return _context.Photos.ToList();
        }
        public Users GetUserWithPhotosById(int id)
        {
            return _context.Users.AsNoTracking().Include(p => p.Photos).FirstOrDefault(p => p.Id == id);
        }
        public Photos getPhotoById(int id)
        {
            return _context.Photos.FirstOrDefault(p =>p.Id == id); 
        }
        public async Task addNewPhoto(int userId, Photos photo)
        {
            if (photo == null)
                throw new ArgumentNullException(nameof(Photos));

            var user = GetUserWithPhotosById(userId);
            user.Photos.Add(photo);
            _context.Photos.Add(photo);
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public async Task setProfilePhoto(Photos photo)
        {
            if (photo == null)
                throw new ArgumentNullException(nameof(Photos));
            photo.IsMainPhoto = true;
            _context.Update(photo);
            _context.SaveChanges();
        }
        public async Task deleteProfilePhoto(Photos photo)
        {
            if (photo == null)
                throw new ArgumentNullException(nameof(Photos));
            photo.IsMainPhoto = false;
            _context.Update(photo);
            _context.SaveChanges();
        }
        public async Task deletePhoto(Photos photo)
        {
            if (photo != null)
            {
                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }


    }
}
