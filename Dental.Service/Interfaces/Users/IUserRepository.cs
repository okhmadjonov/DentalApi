using Dental.Domain.Configurations;
using Dental.Domain.Dtos.User;
using Dental.Domain.Entities.Users;
using Dental.Domain.Models.UserModels;
using System.Linq.Expressions;

namespace Dental.Service.Interfaces.Users;

public interface IUserRepository
{
    ValueTask<IEnumerable<UserModel>> GetAll(PaginationParams @params, Expression<Func<User, bool>> expression = null);
    ValueTask<UserModel> GetAsync(Expression<Func<User, bool>> expression);
    ValueTask<UserModel> CreateAsync(UserForCreationDto userForCreationDTO);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<UserModel> UpdateAsync(int id, UserForCreationDto userForUpdateDTO);
}

