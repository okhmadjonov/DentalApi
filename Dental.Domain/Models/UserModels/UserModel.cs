using Dental.Domain.Entities.Users;

namespace Dental.Domain.Models.UserModels;

public class UserModel
{

    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual UserModel MapFromEntity(User entity)
    {
        Id = entity.Id;
        CreatedAt = entity.CreatedAt;
        UpdatedAt = entity.UpdatedAt == DateTime.MinValue ? null : entity.UpdatedAt;
        Username = entity.Username;
        Email = entity.Email;
        Password = entity.Password;
        return this;
    }
}
