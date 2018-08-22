using Microsoft.EntityFrameworkCore;
using Ms.LocationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms.LocationService.Persistence
{
    public class LocationRepository : ILocationRepository
    {
        private LocationDbContext _context;
        public LocationRepository(LocationDbContext context)
        {
            _context = context;
        }

        public async Task<Location> Add(Location location)
        {
            _context.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<IEnumerable<Location>> AllForMember(Guid memberId)
        {
            return await Task.Run(() => _context.Locations
               .Where(x => x.MemberId == memberId)
               .OrderBy(x => x.Timestamp)
               .ToList());
        }

        public async Task<Location> Delete(Guid memberId, Guid recordId)
        {
            var location = await Get(memberId, recordId);
            _context.Remove(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<Location> Get(Guid memberId, Guid recordId)
        {
            return await Task.Run(() => _context.Locations.Single(x => x.MemberId == memberId
                                 && x.LocationId == recordId));
        }

        public async Task<Location> GetLatestForMember(Guid memberId)
        {
            return await Task.Run(()=> _context.Locations.Where(x => x.MemberId == memberId)
                .OrderBy(x => x.Timestamp)
                .Last());   
        }

        public async Task<Location> Update(Location location)
        {
            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return location;
        }
    }
}
