using Dental.Domain.Commons;

namespace Dental.Domain.Entities.Users;

public class User : Auditable
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
