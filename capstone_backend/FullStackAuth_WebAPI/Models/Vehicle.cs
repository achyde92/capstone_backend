using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
	public class Vehicle
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public bool WheelchairAccess { get; set; }

        [ForeignKey("Driver")]
        public string DriverId { get; set; }

        public User Driver { get; set; }
    }
}

