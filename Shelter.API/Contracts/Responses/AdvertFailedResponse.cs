using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.API.Contracts.Responses
{
    public class AdvertFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
