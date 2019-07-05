using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shelter.Data;

namespace Shelter.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public UserRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _appDbContext.AppUsers.ToArray();
        }

        public IdentityUser GetUserById(string UserId)
        {
            return _appDbContext.AppUsers.FirstOrDefault(p => p.Id == UserId);
        }

        public string GetUserEmailById(string UserId)
        {
            return _appDbContext.AppUsers.FirstOrDefault(p => p.Id == UserId).Email;
        }
    }
}
