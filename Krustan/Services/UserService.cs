using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Krustan.Models;
using Microsoft.EntityFrameworkCore;

namespace Krustan.Services
{
    public class UserService : IUserService
    {
        private readonly KrustanDbContext _context;

        public UserService(KrustanDbContext ctx)
        {
            this._context = ctx;
        }

        public Task<AppUser> AddUser(string uniqueId, string name, string email, 
            string description, DateTime birthdate, string nickname, string profileImage)
        {
            return Task.Run(async () => {
                AppUser user = new AppUser() {
                    Name = name,
                    UniqueId = uniqueId,
                    Email =email,
                    Description = description,
                    Birthdate = birthdate,
                    Nickname = nickname,
                    ProfileImage = profileImage
                };

                _context.AppUsers.Add(user);
                await _context.SaveChangesAsync();

                return _context.AppUsers.First(u => u.UniqueId.Equals(uniqueId));
            });
        }

        public Task<AppUser> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetUserByUniqueId(string uniqueId)
        {
            return Task.Run(() => _context.AppUsers.Include(u => u.Dogs).Where(u => u.UniqueId.Equals(uniqueId)).FirstOrDefault() );
        }

        public Task<bool> SetBirthday(AppUser user, DateTime birthdate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetDescription(AppUser user, string description)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetNickname(AppUser user, string newNickname)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetUsername(AppUser user, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
