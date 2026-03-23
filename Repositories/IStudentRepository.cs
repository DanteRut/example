namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public interface IStudentRepository
{
    void Add(Student student);
    Student? GetById(int id);
    IReadOnlyList<Student> GetAll();
}