using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synt_W1_P1.DTOs;
using Synt_W1_P1.Infrastructure;
using Synt_W1_P1.Interfaces;
using Synt_W1_P1.Models;
using Synt_W1_P1.Services.Users;

namespace Synt_W1_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPassController : ControllerBase
    {
        private readonly IRepository<UserPass> _repo;
        private readonly AppDbContext _context;

        public UserPassController(IRepository<UserPass> repo, AppDbContext context) 
        {
            _repo = repo;
            _context = context;
        }

        [HttpPost]
        public ActionResult Register([FromBody] RegisterationDTO model)
        {
            if (_context.Userss.Any(u => u.Email == model.Email))
                return BadRequest("User with this email already exists!");

            PasswordHasher.createHashedPassword(model.Password, out var passwordHash, out var salt);

            var user = new UserPass
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = salt
            };

            _context.Userss.Add(user);
            _context.SaveChanges();

            return Ok(new { Message = "User registered successfully" });
        }
    }
}
