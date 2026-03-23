namespace UniversityApp.Entities;

public class Grade
{
    private int _value;

    public Grade(int studentId, string subjectId, int value, DateTime date, GradeType type)
    {
        StudentId = studentId;
        SubjectId = subjectId;
        Value = value;   // вызов свойства — проверка диапазона
        Date = date;
        Type = type;
    }

    /// <summary>Идентификатор студента.</summary>
    public int StudentId { get; set; }

    /// <summary>Идентификатор предмета.</summary>
    public string SubjectId { get; set; } = "";

    /// <summary>Дата выставления оценки.</summary>
    public DateTime Date { get; set; }

    /// <summary>Тип оценки (экзамен, контрольная, …).</summary>
    public GradeType Type { get; set; }

    /// <summary>
    /// Значение оценки 1–5. Проверка в set — нельзя записать число вне диапазона
    /// (oop/02_Encapsulation).
    /// </summary>
    public int Value
    {
        get => _value;
        set
        {
            if (value < 1 || value > 5)
                throw new ArgumentException("Оценка должна быть от 1 до 5.");
            _value = value;
        }
    }
}