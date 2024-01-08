using System;
namespace FullStackAuth_WebAPI.DataTransferObjects
{
	public class ReviewWithUserDTO
        {
            public UserForDisplayDto Users { get; set; }
            public string Review { get; set; }
            public double Rating { get; set; }
            public string DriverId { get; set; }
            public int RideReviewId { get; set; }
        }

}

