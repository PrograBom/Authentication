using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Dtos;

public class UserLoginDto
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
