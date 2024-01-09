using System;
namespace FullStackAuth_WebAPI.DataTransferObjects
{
	public class ReviewWithUserDTO
        {
            public UserForDisplayDto Rider { get; set; }
            public string Review { get; set; }
            public double Rating { get; set; }
            public UserForDisplayDto Driver { get; set; }
            public int RideReviewId { get; set; }
        }

}

