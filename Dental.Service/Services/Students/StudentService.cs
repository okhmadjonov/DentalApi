using Dental.Domain.Configurations;
using Dental.Domain.Dtos.Student;
using Dental.Domain.Entities.Student;
using Dental.Domain.Entities.Users;
using Dental.Domain.Interfaces;
using Dental.Domain.Models.StudentModels;
using Dental.Domain.Models.UserModels;
using Dental.Service.Exceptions;
using Dental.Service.Interfaces.Students;
using Dental.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.WebSockets;

namespace Dental.Service.Services.Students;

public class StudentService : IStudentRepository
{
    private readonly IGenericRepository<Student> _studentRepository;

    public StudentService(IGenericRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async ValueTask<StudentModel> CreateAsync(StudentDto studentDto)
    {
        var existStudent = await _studentRepository.GetAsync(u => u.Name_uz == studentDto.Name_uz);

        if (existStudent is not null)
        {
            throw new DentalException(404, "user_not_found");
        }
       // 
       var model = new Student();

        model.Name_uz = studentDto.Name_uz;
        model.Name_ru = studentDto.Name_ru;
        model.Name_en = studentDto.Name_en;
        model.IsDeleted = studentDto.IsDeleted;

        var createdStudent = await _studentRepository.CreateAsync(model);
        await _studentRepository.SaveChangesAsync();
        return new StudentModel().MapFromEntity(createdStudent);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findStudent = await _studentRepository.GetAsync(p => p.Id == id);
        if (findStudent is null)
        {
            throw new DentalException(404, "student_not_found");
        }
        await _studentRepository.DeleteAsync(id);
        await _studentRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<StudentModel>> GetAll(PaginationParams @params, Expression<Func<Student, bool>> expression = null)
    {
        var students = _studentRepository.GetAll(expression: expression, isTracking: false);
        var studentsList = await students.ToPagedList(@params).ToListAsync();
        return studentsList.Select(e => new StudentModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<StudentModel> GetAsync(Expression<Func<Student, bool>> expression)
    {
        var student = await _studentRepository.GetAsync(expression);
        if (student is null)
            throw new DentalException(404, "student_not_found");

        return new StudentModel().MapFromEntity(student);
    }

    public async ValueTask<StudentModel> UpdateAsync(int id, StudentDto studentDto)
    {
        var student = await _studentRepository.GetAsync(u => u.Id == id);

        if (student is null)
            throw new DentalException(404, "student_not_found");

        if (student.Name_uz != studentDto.Name_uz)
            student.Name_uz = studentDto.Name_uz;

        if (student.Name_ru != studentDto.Name_ru)
            student.Name_ru = studentDto.Name_ru;

        if (student.Name_en != studentDto.Name_en)
            student.Name_en = studentDto.Name_en;

        if (student.IsDeleted != studentDto.IsDeleted)
            student.IsDeleted = studentDto.IsDeleted;

        student.UpdatedAt = DateTime.UtcNow;
        await _studentRepository.SaveChangesAsync();
        return new StudentModel().MapFromEntity(student);
    }
}
