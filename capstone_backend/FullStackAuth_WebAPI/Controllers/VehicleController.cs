using System;
using System.Security.Claims;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/vehicle
        [HttpGet("wheelchairaccessible")]
        public ActionResult<IEnumerable<VehicleWithUserDTO>> GetWheelchairAccessibleVehicles()
        {
            var vehiclesWithUsers = _context.Vehicles
                .Include(v => v.Driver)
                .Where(v => v.WheelchairAccess)
                .Select(v => new VehicleWithUserDTO
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    WheelchairAccess = v.WheelchairAccess,
                    Driver = new UserForDisplayDto
                    {
                        Id = v.Driver.Id,
                        FirstName = v.Driver.FirstName,
                        LastName = v.Driver.LastName,
                        UserName = v.Driver.UserName,
                    }
                })
                .Where(v => v.WheelchairAccess)
                .ToList();

            return vehiclesWithUsers;
        }


        // POST api/registervehicle
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Vehicle data)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                data.DriverId = userId;

                _context.Vehicles.Add(data);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
