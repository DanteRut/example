// Сервис студентов и групп

namespace UniversityApp.Services;

using UniversityApp.Dto;
using UniversityApp.Entities;
using UniversityApp.Repositories;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IGroupRepository _groupRepo;

    public StudentService(IStudentRepository studentRepo, IGroupRepository groupRepo)
    {
        _studentRepo = studentRepo;
        _groupRepo = groupRepo;
    }

    public void AddGroup(string id, string name, int courseYear)
    {
        _groupRepo.Add(new Group(id, name, courseYear));
    }

    public void AddStudent(int id, string name, string email, string groupId)
    {
        _studentRepo.Add(new Student(id, name, email, groupId));
    }

    // Where — фильтрация по группе (collection/06_LINQ_Basics).
    public IReadOnlyList<Student> GetStudentsByGroup(string groupId)
    {
        return _studentRepo.GetAll()
            .Where(s => s.GroupId == groupId)
            .ToList();
    }

    // Where + Contains — поиск по имени (collection/06_LINQ_Basics).
    public IReadOnlyList<Student> SearchByName(string part)
    {
        return _studentRepo.GetAll()
            .Where(s => s.Name.Contains(part, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // GroupBy + Count — количество студентов по группам (collection/08_LINQ_Grouping_And_Aggregation).
    public IEnumerable<StudentCountResult> GetStudentCountByGroup()
    {
        var groups = _groupRepo.GetAll().ToDictionary(g => g.Id, g => g.Name);
        return _studentRepo.GetAll()
            .GroupBy(s => s.GroupId)
            .Select(g =>
            {
                var name = groups.TryGetValue(g.Key, out var n) ? n : g.Key;
                return new StudentCountResult { GroupName = name, Count = g.Count() };
            });
    }
}