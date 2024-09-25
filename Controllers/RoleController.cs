using Microsoft.AspNetCore.Mvc;
using Authentication.Data;
using Authentication.Dtos;
using Authentication.Models;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
        {
            if (roleDto == null || string.IsNullOrEmpty(roleDto.Name))
            {
                return BadRequest("Role name is required.");
            }

            // Check if role already exists
            var existingRole = _context.Roles.FirstOrDefault(r => r.Name == roleDto.Name);
            if (existingRole != null)
            {
                return BadRequest("Role already exists.");
            }

            // Vytvorenie novej entity Role
            var newRole = new Role
            {
                Name = roleDto.Name
            };

            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Role added successfully", role = newRole });
        }
    }
}
