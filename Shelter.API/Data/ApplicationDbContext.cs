using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shelter.API;
using Shelter.API.Domains;
using Shelter.API.Entities;

namespace Shelter.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<IdentityUser> AppUsers { get; set; }
        //public DbSet<IdentityUser<Email>> Users { get; set; }
    }
}
