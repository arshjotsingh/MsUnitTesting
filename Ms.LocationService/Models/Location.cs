using System;

namespace Ms.LocationService.Models
{
    public class Location
    {
        public Guid LocationId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid MemberId { get; set; }
    }
}
