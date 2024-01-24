using System;

namespace FullStackAuth_WebAPI.DTOs
{
    public class RideRequestDTO
    {
        public LocationDTO StartLocation { get; set; }
        public LocationDTO EndLocation { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public bool WheelchairAccess { get; set; }
        public bool IsAccepted { get; set; }
        public string Status { get; set; }
    }

    public class LocationDTO
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}


