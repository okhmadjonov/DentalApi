using Dental.Domain.Entities.Users;

namespace Dental.Service.Interfaces.Tokens;

public interface ITokenRepository
{
    string CreateToken(User user);
}