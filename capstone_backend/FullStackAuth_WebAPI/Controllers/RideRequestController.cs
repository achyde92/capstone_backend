using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using FullStackAuth_WebAPI.DTOs;
using System.Security.Claims;
using static FullStackAuth_WebAPI.Models.RideRequest;

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
        public IActionResult Post([FromBody] RideRequestDTO rideRequestDTO)
        {
            try
            {
                string userMakingRequestId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userMakingRequestId))
                {
                    return Unauthorized();
                }

                var rideRequest = new RideRequest
                {
                    StartLocation = new Location
                    {
                        lat = rideRequestDTO.StartLocation.lat,
                        lng = rideRequestDTO.StartLocation.lng
                    },
                    EndLocation = new Location
                    {
                        lat = rideRequestDTO.EndLocation.lat,
                        lng = rideRequestDTO.EndLocation.lng
                    },
                    Date = rideRequestDTO.Date,
                    Time = rideRequestDTO.Time,
                    IsAccepted = false,
                    Status = "Pending",
                    UserMakingRequestId = userMakingRequestId
                };

                _context.Requests.Add(rideRequest);
                _context.SaveChanges();

                return StatusCode(201, rideRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT api/riderequests/{id}/status
        [HttpPut("{id}/status"), Authorize]
        public IActionResult UpdateRideRequestStatus(int id, [FromBody] RideRequestDTO updatedStatus)
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

                rideRequest.Status = updatedStatus.Status;

                _context.SaveChanges();

                return Ok(rideRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // POST /api/riderequests/accept/{id}
        [HttpPost("accept/{id}"), Authorize]
        public IActionResult AcceptRideRequest(int id)
        {
            try
            {
                var rideRequest = _context.Requests.Find(id);

                if (rideRequest == null)
                {
                    return NotFound($"RideRequest with ID {id} not found.");
                }

                rideRequest.Status = "Accepted";

                _context.SaveChanges();

                return Ok($"RideRequest with ID {id} has been accepted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
