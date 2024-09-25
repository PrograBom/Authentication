using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public required string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public DateTime? LastLogin { get; set; }

    public bool IsActive { get; set; } = true;

    public string? ResetPasswordToken { get; set; }

    public DateTime? ResetPasswordExpires { get; set; }

    // FK k roli
    [ForeignKey("Role")]
    public int RoleId { get; set; }

    // Navigačná vlastnosť na rolu
    [Required]
    [StringLength(50)]
    public Role Role { get; set; }
}
