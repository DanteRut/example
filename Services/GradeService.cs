// Сервис оценок и предметов

namespace UniversityApp.Services;

using UniversityApp.Dto;
using UniversityApp.Entities;
using UniversityApp.Repositories;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _gradeRepo;
    private readonly IStudentRepository _studentRepo;
    private readonly ISubjectRepository _subjectRepo;

    public GradeService(IGradeRepository gradeRepo, IStudentRepository studentRepo, ISubjectRepository subjectRepo)
    {
        _gradeRepo = gradeRepo;
        _studentRepo = studentRepo;
        _subjectRepo = subjectRepo;
    }

    public void AddSubject(string id, string name)
    {
        _subjectRepo.Add(new Subject(id, name));
    }

    public void AddGrade(int studentId, string subjectId, int value, DateTime date, GradeType type)
    {
        _gradeRepo.Add(new Grade(studentId, subjectId, value, date, type));
    }

    // Where — оценки конкретного студента (collection/06_LINQ_Basics).
    public IReadOnlyList<Grade> GetGradesByStudent(int studentId)
    {
        return _gradeRepo.GetAll()
            .Where(g => g.StudentId == studentId)
            .ToList();
    }

    // GroupBy + Average — средний балл по каждому предмету (collection/08).
    public IEnumerable<SubjectAverageResult> GetAverageBySubject()
    {
        var subjects = _subjectRepo.GetAll().ToDictionary(s => s.Id, s => s.Name);
        return _gradeRepo.GetAll()
            .GroupBy(g => g.SubjectId)
            .Select(g =>
            {
                var name = subjects.TryGetValue(g.Key, out var n) ? n : g.Key;
                return new SubjectAverageResult
                {
                    SubjectName = name,
                    Average = g.Average(x => x.Value)
                };
            });
    }

    // GroupBy + Average + OrderByDescending + Take — топ студентов (collection/07, 08).
    public IReadOnlyList<StudentAverageResult> GetTopStudents(int count)
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
            .OrderByDescending(r => r.Average)
            .Take(count)
            .ToList();
    }

    // OrderByDescending + Take — последние N оценок (collection/07).
    public IReadOnlyList<Grade> GetRecentGrades(int count)
    {
        return _gradeRepo.GetAll()
            .OrderByDescending(g => g.Date)
            .Take(count)
            .ToList();
    }
}