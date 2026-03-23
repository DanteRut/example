// ==================== Repositories/IStudentRepository.cs ====================
// Абстракция доступа к студентам. D-SOLID (oop/11_SOLID_Dependency_Inversion).

namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public interface IStudentRepository
{
    void Add(Student student);
    Student? GetById(int id);
    IReadOnlyList<Student> GetAll();
}