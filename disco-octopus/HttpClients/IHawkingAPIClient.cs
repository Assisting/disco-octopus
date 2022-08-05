using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disco_octopus.HttpClients
{
    public interface IHawkingAPIClient
    {
        public Task<HawkingResponse?> FindDatesFromString(string inputText);
    }
}
