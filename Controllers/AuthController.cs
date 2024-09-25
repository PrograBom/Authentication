using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authentication.Data;
using Authentication.Dtos;
using Authentication.Models;
using Authentication.Services;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Authentication.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordService _passwordService;
    private readonly IConfiguration _configuration;

    public AuthController(ApplicationDbContext context, PasswordService passwordService, IConfiguration configuration)
    {
        _context = context;
        _passwordService = passwordService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto registerDto)
    {
        var userExists = _context.Users.Any(u => u.Email == registerDto.Email);
        if (userExists)
        {
            return BadRequest("User already exists.");
        }

        var role = await _context.Roles.FindAsync(registerDto.RoleId);
        if (role == null)
        {
            return BadRequest("Invalid role.");
        }

        var newUser = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            RoleId = registerDto.RoleId
        };
        newUser.PasswordHash = _passwordService.HashPassword(newUser, registerDto.Password);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return Ok("User registered successfully.");
    }

    // Endpoint na prihlásenie
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto loginDto)
    {
        // Nájdenie používateľa podľa emailu
        var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null || !_passwordService.VerifyPassword(user, loginDto.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid email or password.");
        }

        // Vytvorenie JWT tokenu
        var tokenHandler = new JwtSecurityTokenHandler();
        // Získanie JWT tajného kľúča z konfigurácie
        var secretKey = _configuration["JWT_SECRET_KEY"];
        // Overenie, či kľúč nie je null alebo prázdny
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new ArgumentNullException(nameof(secretKey), "JWT secret key is not configured.");
        }

        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? "")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}