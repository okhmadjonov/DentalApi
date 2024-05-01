using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Dental.Domain.Entities.Roles;

namespace Dental.Infrastructure.Configurations;

internal sealed class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(DefaultRoles);
    }
    private Role[] DefaultRoles = new[]
    {
        new Role("SuperAdmin")
        {
            Id = 1,
            CreatedAt = new DateTime(2024, 01, 01, 16, 13, 56, 461, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2024, 01, 01, 16, 13, 56, 461, DateTimeKind.Utc),
            IsActive = true
        }
    };
}
