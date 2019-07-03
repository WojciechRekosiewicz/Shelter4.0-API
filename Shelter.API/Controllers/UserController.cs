using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shelter.API.Contracts.Responses;
using Shelter.API.Data.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shelter.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult List()
        {
            var users = _userRepository.GetAllUsers();

            if(!users.Any())
            {
                var errors = new List<string> { "There is no users" };
                return BadRequest(new UserFailedResponse { Errors = errors });
            }

            return Ok(new UserSuccessResponse { Result = users, Message = "Request proceed successfully" });
        }
    }
}
