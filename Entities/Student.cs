namespace UniversityApp.Entities;

public class Student
{
    private string _email = "";

    public Student(int id, string name, string email, string groupId)
    {
        Id = id;
        Name = name;
        Email = email;   // вызов свойства — проверка внутри set
        GroupId = groupId;
    }

    /// <summary>Уникальный идентификатор студента.</summary>
    public int Id { get; }

    /// <summary>ФИО студента.</summary>
    public string Name { get; set; } = "";

    /// <summary>Идентификатор группы, в которой учится студент.</summary>
    public string GroupId { get; set; } = "";

    /// <summary>
    /// Email студента. При записи проверяется формат (oop/02_Encapsulation).
    /// </summary>
    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                throw new ArgumentException("Некорректный email.");
            _email = value;
        }
    }
}