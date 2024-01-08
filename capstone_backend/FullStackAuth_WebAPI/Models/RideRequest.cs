using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		public string Status { get; set; }
	}
}

