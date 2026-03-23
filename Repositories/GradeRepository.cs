namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public class GradeRepository : IGradeRepository
{
    /// <summary>Список оценок в памяти.</summary>
    private readonly List<Grade> _grades = [];

    public void Add(Grade grade) => _grades.Add(grade);

    public IReadOnlyList<Grade> GetAll() => _grades;
}