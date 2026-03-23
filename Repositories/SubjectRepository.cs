// ==================== Repositories/SubjectRepository.cs ====================
// «База» предметов — Dictionary (collection/03_Dictionary).

namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public class SubjectRepository : ISubjectRepository
{
    /// <summary>Словарь предметов по id.</summary>
    private readonly Dictionary<string, Subject> _subjects = [];

    public void Add(Subject subject) => _subjects[subject.Id] = subject;

    public Subject? GetById(string id) => _subjects.TryGetValue(id, out var s) ? s : null;

    public IReadOnlyList<Subject> GetAll() => _subjects.Values.ToList();
}