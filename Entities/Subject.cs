// ==================== Entities/Subject.cs ====================
// Учебный предмет: идентификатор и название. Справочник.
// oop/01_Classes_And_Objects.

namespace UniversityApp.Entities;

public class Subject
{
    public Subject(string id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>Уникальный идентификатор предмета (например, "prog").</summary>
    public string Id { get; }

    /// <summary>Название предмета.</summary>
    public string Name { get; set; } = "";
}