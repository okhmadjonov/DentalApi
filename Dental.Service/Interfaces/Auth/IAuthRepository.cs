using Dental.Domain.Dtos.User;
using Dental.Domain.Models.UserModels;

namespace Dental.Service.Interfaces.Auth;

public interface IAuthRepository
{
    ValueTask<UserModel> Registration(UserForCreationDto user);
    Task<string> Login(LoginDto loginDto);
}