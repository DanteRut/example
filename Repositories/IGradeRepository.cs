// ==================== Repositories/IGradeRepository.cs ====================
namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public interface IGradeRepository
{
    void Add(Grade grade);
    IReadOnlyList<Grade> GetAll();
}