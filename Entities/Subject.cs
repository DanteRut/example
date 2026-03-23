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