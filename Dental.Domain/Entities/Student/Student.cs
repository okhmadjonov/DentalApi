using Dental.Domain.Commons;

namespace Dental.Domain.Entities.Student;

public class Student: Auditable
{

 
    public string Name_en { get; set; }
    public string Name_ru { get; set; }
    public string Name_uz { get; set; }
    public int IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


   }
