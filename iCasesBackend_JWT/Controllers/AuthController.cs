using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BCrypt.Net;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == req.Username))
                return BadRequest(new { message = "Username already exists" });

            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == req.Email))
                return BadRequest(new { message = "Email already exists" });

            var user = new User
            {
                Username = req.Username,
                Email = req.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                ProfilePic = req.ProfilePic // Store profile picture URL
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid credentials" });

            var token = GenerateJwtToken(user);
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                Username = user.Username,
                Email = user.Email,
                ProfilePic = user.ProfilePic
            });
        }

        [HttpGet("check-username/{username}")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            var exists = await _context.Users.AnyAsync(u => u.Username == username);
            return Ok(new { exists });
        }

        [HttpGet("check-email/{email}")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == email);
            return Ok(new { exists });
        }

        // ==================== MANAGEMENT ENDPOINTS (Development Only) ====================

        /// <summary>
        /// Get all users (without password hashes) - Development only
        /// </summary>
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.Email,
                    u.ProfilePic,
                    u.CreatedAt
                })
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
            
            return Ok(new { 
                count = users.Count,
                users 
            });
        }

        /// <summary>
        /// Clear all users from database - Development only
        /// </summary>
        [HttpDelete("clear-users")]
        public async Task<IActionResult> ClearAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            var count = users.Count;
            
            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();
            
            return Ok(new { 
                message = $"Successfully deleted {count} user(s)",
                deletedCount = count 
            });
        }

        /// <summary>
        /// Delete a specific user by ID
        /// </summary>
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });
            
            var username = user.Username;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            
            return Ok(new { message = $"User '{username}' (ID: {id}) deleted successfully" });
        }

        /// <summary>
        /// Get total user count
        /// </summary>
        [HttpGet("users/count")]
        public async Task<IActionResult> GetUserCount()
        {
            var count = await _context.Users.CountAsync();
            return Ok(new { totalUsers = count });
        }

        /// <summary>
        /// Update user email
        /// </summary>
        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateEmail(UpdateEmailRequest req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // Check if new email is already taken by another user
            var emailExists = await _context.Users.AnyAsync(u => u.Email == req.NewEmail && u.Id != user.Id);
            if (emailExists)
                return BadRequest(new { message = "Email already registered" });

            user.Email = req.NewEmail;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Email updated successfully", email = user.Email });
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}