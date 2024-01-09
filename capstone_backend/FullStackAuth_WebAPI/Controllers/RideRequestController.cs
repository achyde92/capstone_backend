using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideRequestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RideRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/riderequests
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] RideRequest data)
        {
            try
            {
                string userMakingRequestId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userMakingRequestId))
                {
                    return Unauthorized();
                }

                data.UserMakingRequestId = userMakingRequestId;

                _context.Requests.Add(data);
                _context.SaveChanges();

                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT api/riderequests/{id}/status
        [HttpPut("{id}/status"), Authorize]
        public IActionResult UpdateRideRequestStatus(int id, [FromBody] RideRequest status)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var rideRequest = _context.Requests.Find(id);

                if (rideRequest == null)
                {
                    return NotFound();
                }

                if (rideRequest.UserAcceptingRequest.Id != userId)
                {
                    return Unauthorized();
                }

                rideRequest.Status = status;

                _context.SaveChanges();

                return Ok(rideRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
