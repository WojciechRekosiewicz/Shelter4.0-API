using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.API.Contracts.Requests
{
    public class UserRegistrationRequest
    {
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
