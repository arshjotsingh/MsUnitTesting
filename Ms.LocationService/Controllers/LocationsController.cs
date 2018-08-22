using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ms.LocationService.Models;
using Ms.LocationService.Persistence;

namespace Ms.LocationService.Controllers
{
    [Produces("application/json")]
    public class LocationsController : Controller
    {
        private ILocationRepository _locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        // GET: api/Locations
        [HttpGet]
        [Route("api/[controller]/{memberId}")]
        public async Task<IActionResult> Get(Guid memberId)
        {
            var locations = await _locationRepository.AllForMember(memberId);
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(locations);
        }

        // GET: api/Locations/5
        [HttpGet]
        [Route("api/[controller]/{memberId}/latest")]
        public async Task<IActionResult> GetLatest(Guid memberId)
        {
            var locations = await _locationRepository.GetLatestForMember(memberId);
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(locations);
        }

        // POST: api/Locations
        [HttpPost]
        [Route("api/[controller]/{memberId}")]
        public async Task<IActionResult> Post(Guid memberId, [FromBody]Location location)
        {
            location.MemberId = memberId;
            location.LocationId = Guid.NewGuid();
            location.Timestamp = DateTime.Now;
            await _locationRepository.Add(location);
            return CreatedAtRoute($"/api/locations/{memberId}/{location.LocationId}", location);
        }

    }
}
