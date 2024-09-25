using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Dtos;

public class UserRegisterDto
{
    [Required]
    [StringLength(50, ErrorMessage = "Name is too long.")]
    public required string Name { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    public required string Password { get; set; }

    [Required]
    public int RoleId { get; set; }
}
