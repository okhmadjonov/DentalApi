using Dental.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Home.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(DefaultUserAdmin);
    }

    private User DefaultUserAdmin => new User()
    {
        Id = 1,
        Username = "Admin",
        Email = "admin@gmail.com",
        Password = "Admin@123?", //.Encrypt(),
        CreatedAt = new DateTime(2024, 01, 01, 16, 13, 56, 461, DateTimeKind.Utc),
        UpdatedAt = new DateTime(2024, 01, 01, 16, 13, 56, 461, DateTimeKind.Utc),
    };
}
