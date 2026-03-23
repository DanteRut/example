// ==================== Services/IGradeService.cs ====================
namespace UniversityApp.Services;

using UniversityApp.Dto;
using UniversityApp.Entities;

public interface IGradeService
{
    void AddSubject(string id, string name);
    void AddGrade(int studentId, string subjectId, int value, DateTime date, GradeType type);
    IReadOnlyList<Grade> GetGradesByStudent(int studentId);
    IEnumerable<SubjectAverageResult> GetAverageBySubject();
    IReadOnlyList<StudentAverageResult> GetTopStudents(int count);
    IReadOnlyList<Grade> GetRecentGrades(int count);
}