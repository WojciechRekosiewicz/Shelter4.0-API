using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.API.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<IdentityUser> GetAllUsers();
        IdentityUser GetUserById(string UserId);
        string GetUserEmailById(string UserId);
    }
}
