using Shelter.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.API.Contracts.Responses
{
    public class AdvertSuccessResponse
    {
        public IEnumerable<Advert> Result { get; set; }
        public string Message { get; set; }
    }
}
