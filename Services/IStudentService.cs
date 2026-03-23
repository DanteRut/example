// ==================== Services/IStudentService.cs ====================
// Абстракция сервиса студентов. Консоль зависит от интерфейса (D-SOLID).

namespace UniversityApp.Services;

using UniversityApp.Dto;
using UniversityApp.Entities;

public interface IStudentService
{
    void AddGroup(string id, string name, int courseYear);
    void AddStudent(int id, string name, string email, string groupId);
    IReadOnlyList<Student> GetStudentsByGroup(string groupId);
    IReadOnlyList<Student> SearchByName(string part);
    IEnumerable<StudentCountResult> GetStudentCountByGroup();
}