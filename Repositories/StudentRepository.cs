namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public class StudentRepository : IStudentRepository
{
    /// <summary>Список студентов в памяти (collection/02_List).</summary>
    private readonly List<Student> _students = [];

    public void Add(Student student) => _students.Add(student);

    public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public IReadOnlyList<Student> GetAll() => _students;
}