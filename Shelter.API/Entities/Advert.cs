using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shelter.API.Entities
{
    public class Advert
    {
        [Key]
        public int AdvertId { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string ReservingId { get; set; }
        [Required]
        [DisplayName("Short Description")]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string ShortDescription { get; set; }
        [Required]
        [DisplayName("Long Description")]
        [StringLength(450, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 30)]
        public string LongDescription { get; set; }
        [Url]
        public string ImageUrl { get; set; }

        [ForeignKey("AuthorId")]
        public IdentityUser AuthorUser { get; set; }
        [ForeignKey("ReservingId")]
        public IdentityUser ReservingUser { get; set; }
    }
}
