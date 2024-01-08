using Microsoft.AspNetCore.Identity;

namespace FullStackAuth_WebAPI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public bool IsEmployee { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string EmergencyContact { get; set; }
        public double Balance { get; set; }
    }
}
