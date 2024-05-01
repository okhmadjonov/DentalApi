using System.ComponentModel.DataAnnotations;

namespace Dental.Domain.Dtos.User;

public class UserForCreationDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
