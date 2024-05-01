using Dental.Domain.Configurations;
using Dental.Domain.Dtos.User;
using Dental.Domain.Entities.Users;
using Dental.Domain.Interfaces;
using Dental.Domain.Models.UserModels;
using Dental.Service.Exceptions;
using Dental.Service.Interfaces.Users;
using Home.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dental.Service.Services.Users;


internal sealed class UserService : IUserRepository
{
    private readonly IGenericRepository<User> _userRepository;

    public UserService(IGenericRepository<User> userRepository)
        => _userRepository = userRepository;

    public async ValueTask<UserModel> CreateAsync(UserForCreationDto user)
    {

        var existUser = await _userRepository.GetAsync(u => u.Email == user.Email);
        string passwordHash = existUser.Password.Encrypt();

        if (existUser != null)
        {
            existUser.Username = user.Username;
            existUser.Email = user.Email;
            existUser.Password = passwordHash;
            existUser.CreatedAt = DateTime.UtcNow;
            existUser.UpdatedAt = DateTime.UtcNow;

        };
        var createdUser = await _userRepository.CreateAsync(existUser);
        await _userRepository.SaveChangesAsync();
        return new UserModel().MapFromEntity(createdUser);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findUser = await _userRepository.GetAsync(p => p.Id == id);
        if (findUser is null)
        {
            throw new DentalException(404, "user_not_found");
        }
        await _userRepository.DeleteAsync(id);
        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<UserModel>> GetAll(PaginationParams @params, Expression<Func<User, bool>> expression = null)
    {
        var users = _userRepository.GetAll(expression: expression, isTracking: false);
        var usersList = await users.ToPagedList(@params).ToListAsync();
        return usersList.Select(e => new UserModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<UserModel> GetAsync(Expression<Func<User, bool>> expression)
    {
        var user = await _userRepository.GetAsync(expression);
        if (user is null)
            throw new DentalException(404, "user_not_found");

        return new UserModel().MapFromEntity(user);
    }

    public async ValueTask<UserModel> UpdateAsync(int id, UserForCreationDto userForUpdateDTO)
    {
        var user = await _userRepository.GetAsync(u => u.Id == id);

        if (user is null)
            throw new DentalException(404, "user_not_found");

        if (userForUpdateDTO.Email != user.Email)
            user.Email = userForUpdateDTO.Email;
        if (userForUpdateDTO.Username != user.Username)
            user.Username = userForUpdateDTO.Username;
        if (!string.IsNullOrEmpty(userForUpdateDTO.Password))
            user.Password = userForUpdateDTO.Password.Encrypt();

        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.SaveChangesAsync();
        return new UserModel().MapFromEntity(user);
    }

}
