using Dental.Domain.Commons;

namespace Dental.Domain.Entities.Roles;

public class Role : Auditable
{
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public Role() { }
    public Role(string name)
    {
        Name = name;
    }
}
