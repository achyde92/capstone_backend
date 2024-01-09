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
        // PUt endpoint accept/deny 
    }
}
