using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.API.Contracts.Requests
{
    public class AdvertCreateRequest
    {
        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Title { get; set; }
        public string AuthorId { get; set; }
        [Required]
        [DisplayName("Short Description")]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string ShortDescription { get; set; }
        [Required]
        [DisplayName("Long Description")]
        [StringLength(450, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 30)]
        public string LongDescription { get; set; }
        [Url, Required]
        public string ImageUrl { get; set; }
    }
}
