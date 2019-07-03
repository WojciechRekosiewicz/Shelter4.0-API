using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shelter.API.Contracts.Responses;
using Shelter.API.Data.Repositories;


namespace Shelter.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
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
                var errors = new List<string> { "There are no users" };
                return BadRequest(new UserFailedResponse { Errors = errors });
            }

            return Ok(new UserSuccessResponse { Result = users, Message = "Request proceed successfully" });
        }

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
