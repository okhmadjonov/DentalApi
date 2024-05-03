using Dental.Domain.Dtos.User;
using Dental.Domain.Entities.Users;
using Dental.Domain.Interfaces;
using Dental.Domain.Models.UserModels;
using Dental.Service.Exceptions;
using Dental.Service.Extentions;
using Dental.Service.Interfaces.Auth;
using Dental.Service.Interfaces.Tokens;


namespace Dental.Service.Services.Auth;


public  class AuthService : IAuthRepository
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly ITokenRepository _tokenGenerator;

    public AuthService(IGenericRepository<User> genericRepository, ITokenRepository tokenGenerator)
    {
        _userRepository = genericRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        if (loginDto.Email != null)
        {
            var user = await _userRepository.GetAsync(u => u.Email == loginDto.Email);

            if (user != null)
            {
                bool verify = Verify(loginDto.Password, user.Password);

                if (verify)
                {
                    return _tokenGenerator.CreateToken(user);
                }
                else
                {
                    throw new DentalException(401, "incorrect_password");
                }
            }
            else
            {
                throw new DentalException(404, "user_not_found");
            }
        }
        throw new DentalException(404, "user_not_found");
    }

    public async ValueTask<UserModel> Registration(UserForCreationDto user)
    {
        var existingUser = await _userRepository.GetAsync(u => u.Email == user.Email);

        if (existingUser == null)
        {
            string passwordHash = user.Password.Encrypt();

            User newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = passwordHash
            };

            var registeredUser = await _userRepository.CreateAsync(newUser);
            await _userRepository.SaveChangesAsync();
            return new UserModel().MapFromEntity(registeredUser);
        }
        else
        {
            throw new DentalException(401, "user_already_exist");
        }
    }
    public static bool Verify(string password, string hashedPassword)
    {
        string hashedInputPassword = password.Encrypt();
        return string.Equals(hashedInputPassword, hashedPassword);
    }
}

