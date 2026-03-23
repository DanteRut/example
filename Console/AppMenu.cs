namespace UniversityApp.Console;

using UniversityApp.Services;

public class AppMenu
{
    private readonly IStudentService _studentService;
    private readonly IGradeService _gradeService;
    private readonly IReportService _reportService;

    public AppMenu(IStudentService studentService, IGradeService gradeService, IReportService reportService)
    {
        _studentService = studentService;
        _gradeService = gradeService;
        _reportService = reportService;
    }

    public void Run()
    {
        while (true)
        {
            PrintMenu();
            var key = System.Console.ReadLine()?.Trim() ?? "";

            if (key == "0") break;

            switch (key)
            {
                case "1":  ShowStudentsByGroup(); break;
                case "2":  ShowSearchByName(); break;
                case "3":  ShowStudentCountByGroup(); break;
                case "4":  ShowGradesByStudent(); break;
                case "5":  ShowAverageBySubject(); break;
                case "6":  ShowTopStudents(); break;
                case "7":  ShowRecentGrades(); break;
                case "8":  ShowSubjectStats(); break;
                case "9":  ShowExcellentStudents(); break;
                case "10": ShowGroupPerformance(); break;
                default:
                    System.Console.WriteLine("Неизвестный пункт.");
                    break;
            }

            System.Console.WriteLine();
        }

        System.Console.WriteLine("Выход.");
    }

    private void PrintMenu()
    {
        System.Console.WriteLine("--- Меню ---");
        System.Console.WriteLine("1  — Студенты по группе");
        System.Console.WriteLine("2  — Поиск студента по имени");
        System.Console.WriteLine("3  — Количество студентов по группам");
        System.Console.WriteLine("4  — Оценки студента");
        System.Console.WriteLine("5  — Средний балл по предметам");
        System.Console.WriteLine("6  — Топ-N студентов по среднему баллу");
        System.Console.WriteLine("7  — Последние N оценок");
        System.Console.WriteLine("8  — Статистика по предметам");
        System.Console.WriteLine("9  — Студенты-отличники");
        System.Console.WriteLine("10 — Успеваемость по группам");
        System.Console.WriteLine("0  — Выход");
        System.Console.Write("Выбор: ");
    }

    // --- 1. Студенты и группы (IStudentService) ---

    private void ShowStudentsByGroup()
    {
        System.Console.Write("Введите ID группы (cs1, cs2, math1): ");
        var groupId = System.Console.ReadLine()?.Trim() ?? "";
        var students = _studentService.GetStudentsByGroup(groupId);
        System.Console.WriteLine($"Студенты группы '{groupId}':");
        foreach (var s in students)
            System.Console.WriteLine($"  #{s.Id}  {s.Name}  ({s.Email})");
        if (students.Count == 0) System.Console.WriteLine("  Нет студентов.");
    }

    private void ShowSearchByName()
    {
        System.Console.Write("Введите часть имени: ");
        var part = System.Console.ReadLine()?.Trim() ?? "";
        var students = _studentService.SearchByName(part);
        System.Console.WriteLine("Найдено:");
        foreach (var s in students)
            System.Console.WriteLine($"  #{s.Id}  {s.Name}  группа {s.GroupId}");
        if (students.Count == 0) System.Console.WriteLine("  Ничего.");
    }

    private void ShowStudentCountByGroup()
    {
        System.Console.WriteLine("Количество студентов по группам:");
        foreach (var item in _studentService.GetStudentCountByGroup())
            System.Console.WriteLine($"  {item.GroupName}: {item.Count}");
    }

    // --- 2. Оценки и предметы (IGradeService) ---

    private void ShowGradesByStudent()
    {
        System.Console.Write("Введите ID студента (1–6): ");
        if (!int.TryParse(System.Console.ReadLine(), out var studentId))
        {
            System.Console.WriteLine("Некорректный ID.");
            return;
        }
        var grades = _gradeService.GetGradesByStudent(studentId);
        System.Console.WriteLine($"Оценки студента #{studentId}:");
        foreach (var g in grades)
            System.Console.WriteLine($"  {g.SubjectId}: {g.Value}  ({g.Type})  {g.Date:dd.MM.yyyy}");
        if (grades.Count == 0) System.Console.WriteLine("  Нет оценок.");
    }

    private void ShowAverageBySubject()
    {
        System.Console.WriteLine("Средний балл по предметам:");
        foreach (var item in _gradeService.GetAverageBySubject())
            System.Console.WriteLine($"  {item.SubjectName}: {item.Average:F2}");
    }

    private void ShowTopStudents()
    {
        System.Console.Write("Сколько студентов показать? (по умолчанию 3): ");
        if (!int.TryParse(System.Console.ReadLine(), out var count) || count <= 0)
            count = 3;
        var list = _gradeService.GetTopStudents(count);
        System.Console.WriteLine($"Топ-{count} по среднему баллу:");
        foreach (var item in list)
            System.Console.WriteLine($"  {item.StudentName}: {item.Average:F2}");
    }

    private void ShowRecentGrades()
    {
        System.Console.Write("Сколько оценок показать? (по умолчанию 5): ");
        if (!int.TryParse(System.Console.ReadLine(), out var count) || count <= 0)
            count = 5;
        var list = _gradeService.GetRecentGrades(count);
        System.Console.WriteLine($"Последние {count} оценок:");
        foreach (var g in list)
            System.Console.WriteLine($"  {g.Date:dd.MM.yyyy}  студент #{g.StudentId}  {g.SubjectId}: {g.Value}  ({g.Type})");
    }

    // --- 3. Отчёты (IReportService) ---

    private void ShowSubjectStats()
    {
        System.Console.WriteLine("Статистика по предметам:");
        foreach (var item in _reportService.GetSubjectStats())
            System.Console.WriteLine($"  {item.SubjectName}: оценок {item.GradeCount}, средний балл {item.AverageGrade:F2}");
    }

    private void ShowExcellentStudents()
    {
        System.Console.Write("Порог среднего балла (по умолчанию 4.5): ");
        if (!double.TryParse(System.Console.ReadLine(), out var threshold) || threshold <= 0)
            threshold = 4.5;
        System.Console.WriteLine($"Студенты со средним баллом >= {threshold:F1}:");
        var list = _reportService.GetExcellentStudents(threshold);
        var found = false;
        foreach (var item in list)
        {
            System.Console.WriteLine($"  {item.StudentName}: {item.Average:F2}");
            found = true;
        }
        if (!found) System.Console.WriteLine("  Нет таких студентов.");
    }

    private void ShowGroupPerformance()
    {
        System.Console.WriteLine("Успеваемость по группам:");
        foreach (var item in _reportService.GetGroupPerformance())
            System.Console.WriteLine($"  {item.GroupName}: студентов {item.StudentCount}, средний балл {item.AverageGrade:F2}");
    }
}