// ==================== Services/ReportService.cs ====================
// Сервис отчётов — аналитика по всем данным. Зависит от четырёх интерфейсов
// репозиториев (D-SOLID). LINQ: GroupBy, Count, Average, Where, OrderByDescending.

namespace UniversityApp.Services;

using UniversityApp.Dto;
using UniversityApp.Repositories;

public class ReportService : IReportService
{
    private readonly IGradeRepository _gradeRepo;
    private readonly IStudentRepository _studentRepo;
    private readonly ISubjectRepository _subjectRepo;
    private readonly IGroupRepository _groupRepo;

    public ReportService(
        IGradeRepository gradeRepo,
        IStudentRepository studentRepo,
        ISubjectRepository subjectRepo,
        IGroupRepository groupRepo)
    {
        _gradeRepo = gradeRepo;
        _studentRepo = studentRepo;
        _subjectRepo = subjectRepo;
        _groupRepo = groupRepo;
    }

    // GroupBy + Count + Average — статистика по предметам (collection/08).
    public IEnumerable<SubjectStatResult> GetSubjectStats()
    {
        var subjects = _subjectRepo.GetAll().ToDictionary(s => s.Id, s => s.Name);
        return _gradeRepo.GetAll()
            .GroupBy(g => g.SubjectId)
            .Select(g =>
            {
                var name = subjects.TryGetValue(g.Key, out var n) ? n : g.Key;
                return new SubjectStatResult
                {
                    SubjectName = name,
                    GradeCount = g.Count(),
                    AverageGrade = g.Average(x => x.Value)
                };
            });
    }

    // GroupBy + Average + Where — студенты со средним баллом >= порога (collection/06, 08).
    public IEnumerable<StudentAverageResult> GetExcellentStudents(double threshold)
    {
        var students = _studentRepo.GetAll();
        return _gradeRepo.GetAll()
            .GroupBy(g => g.StudentId)
            .Select(g =>
            {
                var name = students.FirstOrDefault(s => s.Id == g.Key)?.Name ?? "?";
                return new StudentAverageResult
                {
                    StudentName = name,
                    Average = g.Average(x => x.Value)
                };
            })
            .Where(r => r.Average >= threshold)
            .OrderByDescending(r => r.Average);
    }

    // GroupBy (студенты по группе) + Where (оценки этих студентов) + Average (collection/06, 08).
    public IEnumerable<GroupPerformanceResult> GetGroupPerformance()
    {
        var groups = _groupRepo.GetAll().ToDictionary(g => g.Id, g => g.Name);
        var students = _studentRepo.GetAll();
        var grades = _gradeRepo.GetAll();

        return students
            .GroupBy(s => s.GroupId)
            .Select(studentGroup =>
            {
                var groupName = groups.TryGetValue(studentGroup.Key, out var n) ? n : studentGroup.Key;
                var studentIds = studentGroup.Select(s => s.Id).ToHashSet();
                var groupGrades = grades.Where(g => studentIds.Contains(g.StudentId));
                var avg = groupGrades.Any() ? groupGrades.Average(g => g.Value) : 0.0;
                return new GroupPerformanceResult
                {
                    GroupName = groupName,
                    StudentCount = studentGroup.Count(),
                    AverageGrade = avg
                };
            });
    }
}