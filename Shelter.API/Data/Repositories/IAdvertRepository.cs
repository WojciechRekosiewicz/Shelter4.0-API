using Microsoft.AspNetCore.Mvc;
using Shelter.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shelter.API.Data.Repositories
{
    public interface IAdvertRepository
    {
        IEnumerable<Advert> GetAllAdverts();
        IEnumerable<Advert> GetAdvertsByUserId(string userId);
        Advert GetAdvertById(int AdvertID);
        Task CreateAsync(Advert advert);
        Task DeleteAsync(int id);
        Task UpdateAsync(Advert advert);
        bool CanDelete(string userId, int advertId);
        Task<bool> UserOwnsAdvertAsync(int advertId, string userId);
    }
}
