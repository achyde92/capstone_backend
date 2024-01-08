using System;
using System.Security.Claims;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RideReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/reviews
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] RideReview data)
        {
            try
            {
                string riderId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(riderId))
                {
                    return Unauthorized();
                }
                data.RiderId = riderId;

                _context.RideReviews.Add(data);
                _context.SaveChanges();
                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}

