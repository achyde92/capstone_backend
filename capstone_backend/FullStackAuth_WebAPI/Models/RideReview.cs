using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
	public class RideReview
	{
        [Key]
        public string Id { get; set; }

        public double Rating { get; set; }
		public string Review { get; set; }

        [ForeignKey("Driver")]
        public string DriverId { get; set; }

        public User Driver { get; set; }

        [ForeignKey("Rider")]
        public string RiderId { get; set; }

        public User Rider { get; set; }

    }
}

