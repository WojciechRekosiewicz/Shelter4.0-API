using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.API.Contracts.Responses
{
    public class UserSuccessResponse
    {
        public IEnumerable<IdentityUser> Result { get; set; }
        public string Message { get; set; }
    }
}
