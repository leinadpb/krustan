using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Krustan.Models;

namespace Krustan.Services
{
    public interface IUserService
    {
        Task<AppUser> GetUserByUniqueId(string uniqueId);
        Task<AppUser> GetUserByEmail(string email);
        Task<bool> SetUsername(AppUser user, string newName);
        Task<bool> SetNickname(AppUser user, string newNickname);
        Task<bool> SetBirthday(AppUser user, DateTime birthdate);
        Task<bool> SetDescription(AppUser user, string description);
        Task<AppUser> AddUser(string uniqueId, string name, string email, string description,
            DateTime birthdate, string nickname, string profileImage);
    }
}
