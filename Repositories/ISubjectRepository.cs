// ==================== Repositories/ISubjectRepository.cs ====================
namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public interface ISubjectRepository
{
    void Add(Subject subject);
    Subject? GetById(string id);
    IReadOnlyList<Subject> GetAll();
}