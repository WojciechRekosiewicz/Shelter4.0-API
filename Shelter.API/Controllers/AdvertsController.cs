using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shelter.API.Data.Repositories;
using Shelter.API.Entities;
using Shelter.API.Extensions;

namespace Shelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : Controller
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AdvertsController(IAdvertRepository advertRepository, UserManager<IdentityUser> userManager)
        {
            _advertRepository = advertRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult List()
        {
            var adverts = _advertRepository.GetAllAdverts();

            return Ok(adverts);
        }

        [HttpGet("my"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult MyAdverts()
        {
            //var adverts = _advertRepository.GetAdvertsByUserId(_userManager.GetUserId(HttpContext.User));

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var advert = _advertRepository.GetAdvertById(id);
            if (advert == null)
            {
                return NotFound();
            }

            return Ok(advert);
        }

        [HttpPost("create"), Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] Advert advert)
        {
            var currentUserId = HttpContext.GetUserId();

            advert.AuthorId = currentUserId;

            await _advertRepository.CreateAsync(advert);

            return Ok();
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] Advert advert)
        {
            var userOwnsAdvert = await _advertRepository.UserOwnsAdvertAsync(advert.AdvertId, HttpContext.GetUserId());

            if(!userOwnsAdvert)
            {
                return BadRequest(new { error = "You do not own this advert." });
            }

            try
            {
                await _advertRepository.UpdateAsync(advert);

                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _advertRepository.DeleteAsync(id);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}