using System;
namespace FullStackAuth_WebAPI.DataTransferObjects
{
	public class VehicleWithUserDTO
	{
        public int Id { get; set; }
        public bool IsEmployee { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool WheelchairAccess { get; set; }
        public UserForDisplayDto Driver { get; set; }
    }
}

