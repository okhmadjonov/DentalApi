using System.ComponentModel.DataAnnotations;

namespace Dental.Domain.Dtos.Student;

public class StudentDto
{
    [Required]
    public string Name_en { get; set; }
    [Required]
    public string Name_ru { get; set; }
    [Required]
    public string Name_uz { get; set; }
    [Required]
    public int IsDeleted { get; set; }
}
