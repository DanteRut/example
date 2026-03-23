// ==================== Entities/GradeType.cs ====================
// Перечисление типов оценок — именованные константы вместо «магических» чисел.
// oop/12_Composition_And_More — раздел Enum.

namespace UniversityApp.Entities;

public enum GradeType
{
    /// <summary>Экзамен.</summary>
    Exam = 0,

    /// <summary>Контрольная / зачёт.</summary>
    Test = 1,

    /// <summary>Домашняя работа.</summary>
    Homework = 2,

    /// <summary>Курсовая работа.</summary>
    CourseWork = 3
}