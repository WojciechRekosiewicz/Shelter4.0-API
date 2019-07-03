using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Shelter.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<IdentityUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IdentityUser GetUserById(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
