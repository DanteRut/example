// ==================== Entities/Group.cs ====================
// Учебная группа: идентификатор, название, номер курса.
// oop/01_Classes_And_Objects — простой класс-сущность.

namespace UniversityApp.Entities;

public class Group
{
    public Group(string id, string name, int courseYear)
    {
        Id = id;
        Name = name;
        CourseYear = courseYear;
    }

    /// <summary>Уникальный идентификатор группы (например, "cs1").</summary>
    public string Id { get; }

    /// <summary>Название группы.</summary>
    public string Name { get; set; } = "";

    /// <summary>Номер курса (1, 2, …).</summary>
    public int CourseYear { get; set; }
}