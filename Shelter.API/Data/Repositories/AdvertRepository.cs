using Microsoft.EntityFrameworkCore;
using Shelter.API.Entities;
using Shelter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.API.Data.Repositories
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public AdvertRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Advert> GetAdvertsByUserId(string id)
        {
            return _appDbContext.Adverts.Where(advert => advert.AuthorId == id).ToArray();
        }

        public IEnumerable<Advert> GetAllAdverts()
        {
            return _appDbContext.Adverts.ToArray();
        }

        public Advert GetAdvertById(int AdvertID)
        {
            return _appDbContext.Adverts.FirstOrDefault(p => p.AdvertId == AdvertID);
        }

        public async Task CreateAsync(Advert advert)
        {
            await Task.WhenAll(_appDbContext.AddAsync(advert), _appDbContext.SaveChangesAsync());
        }

        public async Task DeleteAsync(int id)
        {
            var record = (from advert in _appDbContext.Adverts
                          where advert.AdvertId == id
                          select advert).FirstOrDefault();

            if (record == null) throw new ArgumentException("Advert does not exist in the database.");

            _appDbContext.Adverts.Remove(record);
            await _appDbContext.SaveChangesAsync();
        }

        public bool CanDelete(string userId, int advertId)
        {
            var record = (from advert in _appDbContext.Adverts
                          where advert.AdvertId == advertId
                          select advert.AuthorId).FirstOrDefault();

            return (record != null) ? record == userId : false;
        }

        public async Task UpdateAsync(Advert advert)
        {
            var record = _appDbContext.Adverts.Where(a => a.AdvertId == advert.AdvertId).FirstOrDefault();

            if (record == null) throw new ArgumentException("Advert does not exist in the database");

            record.Title = advert.Title;
            record.ShortDescription = advert.ShortDescription;
            record.LongDescription = advert.LongDescription;
            record.ImageUrl = advert.ImageUrl;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task Reserve(Advert advert)
        {
            var record = _appDbContext.Adverts.Where(a => a.AdvertId == advert.AdvertId).FirstOrDefault();

            record.ReservingId = advert.ReservingId;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> UserOwnsAdvertAsync(int advertId, string userId)
        {
            var advert = await _appDbContext.Adverts.AsNoTracking().SingleOrDefaultAsync(x => x.AdvertId == advertId);

            if(advert == null)
            {
                return false;
            }

            return advert.AuthorId == userId;
        }

    }
}
