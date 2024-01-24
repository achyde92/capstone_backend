using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace FullStackAuth_WebAPI.Models
{
	public class RideRequest
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("UserMakingRequest")]
		public string UserMakingRequestId { get; set; }
		public User UserMakingRequest { get; set; }

		[ForeignKey("UserAcceptingRequest")]
		public string UserAcceptingRequestId { get; set; }
		public User UserAcceptingRequest { get; set; }

        public Location StartLocation { get; set; }
        public Location EndLocation { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public bool WheelchairAccess { get; set; }

        public bool IsAccepted { get; set; } 
        public string Status { get; set; }

        public class Location
        {
            public int Id { get; set; }
            public double lat { get; set; }
            public double lng{ get; set; }
        }
    }
}

