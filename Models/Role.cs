using System.ComponentModel.DataAnnotations;

namespace Authentication.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    // Navigačná vlastnosť, ktorá zabezpečí vzťah medzi používateľmi a rolami
    public ICollection<User>? Users { get; set; }

    public static implicit operator string(Role v)
    {
        return v.Name;
    }
}
