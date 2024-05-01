using Dental.Domain.Commons;
using Dental.Domain.Entities.Users;

namespace Dental.Domain.Entities.Roles;

public class UserRole : Auditable
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}
