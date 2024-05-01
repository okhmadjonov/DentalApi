using Dental.Domain.Configurations;
using Dental.Domain.Dtos.Student;
using Dental.Domain.Entities.Student;
using Dental.Domain.Models.StudentModels;
using System.Linq.Expressions;

namespace Dental.Service.Interfaces.Students;

public interface IStudentRepository
{
    ValueTask<IEnumerable<StudentModel>> GetAll(PaginationParams @params, Expression<Func<Student, bool>> expression = null);
    ValueTask<StudentModel> GetAsync(Expression<Func<Student, bool>> expression);
    ValueTask<StudentModel> CreateAsync(StudentDto studentDto);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<StudentModel> UpdateAsync(int id, StudentDto studentDto);
}
