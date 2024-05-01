using Dental.Domain.Entities.Student;
namespace Dental.Domain.Models.StudentModels;

public class StudentModel
{

    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Name_en { get; set; }
    public string Name_ru { get; set; }
    public string Name_uz { get; set; }
    public int IsDeleted { get; set; }
    public virtual StudentModel MapFromEntity(Student entity)
    {
        Id = entity.Id;
        CreatedAt = entity.CreatedAt;
        UpdatedAt = entity.UpdatedAt == DateTime.MinValue ? null : entity.UpdatedAt;
        Name_en = entity.Name_en;
        Name_ru = entity.Name_ru;
        Name_uz = entity.Name_uz;
        IsDeleted = entity.IsDeleted;   
        return this;
    }
}