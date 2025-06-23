using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetalPeel.Models.DTOs;

public class UserProfileDTO
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string Address { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

    public string? Password { get; set; }

    public List<string>? Roles { get; set; }

    public string? IdentityUserId { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}
