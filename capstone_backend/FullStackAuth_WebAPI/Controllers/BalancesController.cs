using System.Security.Claims;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BalanceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BalanceController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET api/balance
    [HttpGet, Authorize]
    public IActionResult GetUserBalance()
    {
        try
        {
            string userId = User.FindFirstValue("id");

            var userBalance = _context.Balances.FirstOrDefault(b => b.UserId == userId);

            if (userBalance != null)
            {
                var balanceDTO = new BalanceDTO
                {
                    Amount = userBalance.Amount
                };

                return Ok(balanceDTO);
            }
            else
            {
                return NotFound("User balance not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}

