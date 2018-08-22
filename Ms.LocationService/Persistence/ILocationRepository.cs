using Ms.LocationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms.LocationService.Persistence
{
    public interface ILocationRepository
    {
        Task<Location> Add(Location location);
        Task<Location> Update(Location location);
        Task<Location> Get(Guid memberId, Guid recordId);
        Task<Location> Delete(Guid memberId, Guid recordId);
        Task<Location> GetLatestForMember(Guid memberId);
        Task<IEnumerable<Location>> AllForMember(Guid memberId);
    }
}
