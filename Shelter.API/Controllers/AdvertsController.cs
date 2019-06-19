using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shelter.API.Contracts.Requests;
using Shelter.API.Contracts.Responses;
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

            if(!adverts.Any())
            {
                var errors = new List<string> { "There are no adverts in the database." };
                return BadRequest(new AdvertFailedResponse { Errors = errors });
            }

            return Ok(new AdvertSuccessResponse { Result = adverts, Message = "Request proceed successfully" });
        }

        [HttpGet("my"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult MyAdverts()
        {
            var adverts = _advertRepository.GetAdvertsByUserId(HttpContext.GetUserId());

            if(!adverts.Any())
            {
                var errors = new List<string> { "There are no adverts of the currect user in the database." };
                return BadRequest(new AdvertFailedResponse { Errors = errors });
            }

            return Ok(new AdvertSuccessResponse { Result = adverts, Message = "Request proceed successfully" });
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
        public async Task<IActionResult> CreateAsync([FromBody] AdvertCreateRequest advertRequest)
        {
            var currentUserId = HttpContext.GetUserId();
            var advert = new Advert
            {
                Title = advertRequest.Title,
                AuthorId = currentUserId,
                ShortDescription = advertRequest.ShortDescription,
                LongDescription = advertRequest.LongDescription,
                ImageUrl = advertRequest.ImageUrl
            };
            

            await _advertRepository.CreateAsync(advert);
            var response = new AdvertSuccessResponse { Message = "Successfully created new advert" };
            return Ok(response);
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] Advert advert )
        {
            var userOwnsAdvert = await _advertRepository.UserOwnsAdvertAsync(advert.AdvertId, HttpContext.GetUserId());

            if(!userOwnsAdvert)
            {
                var response = new AdvertFailedResponse { Errors = new List<string> { "You do not own this advert." } };
                return BadRequest(response);
            }

            try
            {
                await _advertRepository.UpdateAsync(advert);
                var response = new AdvertSuccessResponse { Message = "Successfully updated the advert." };
                return Ok(response);
            }
            catch(ArgumentException ex)
            {
                var response = new AdvertFailedResponse { Errors = new List<string> { ex.Message } };
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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