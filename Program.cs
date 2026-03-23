// ==================== Program.cs ====================
// Точка входа. Сборка: конкретные репозитории → сервисы (через интерфейсы) → меню.
// D-SOLID: oop/11_SOLID_Dependency_Inversion — создание на «краю» приложения.

using UniversityApp.Console;
using UniversityApp.Entities;
using UniversityApp.Repositories;
using UniversityApp.Services;

// 1. Репозитории (слой «база данных» — коллекции в памяти).
var studentRepo = new StudentRepository();
var groupRepo = new GroupRepository();
var subjectRepo = new SubjectRepository();
var gradeRepo = new GradeRepository();

// 2. Сервисы зависят от интерфейсов репозиториев (D-SOLID).
IStudentService studentService = new StudentService(studentRepo, groupRepo);
IGradeService gradeService = new GradeService(gradeRepo, studentRepo, subjectRepo);
IReportService reportService = new ReportService(gradeRepo, studentRepo, subjectRepo, groupRepo);

// 3. Заполняем тестовыми данными.
SeedData(studentService, gradeService);

// 4. Консоль зависит только от интерфейсов сервисов.
var menu = new AppMenu(studentService, gradeService, reportService);
menu.Run();

static void SeedData(IStudentService studentService, IGradeService gradeService)
{
    // --- Группы ---
    studentService.AddGroup("cs1",   "Информатика 1 курс", 1);
    studentService.AddGroup("cs2",   "Информатика 2 курс", 2);
    studentService.AddGroup("math1", "Математика 1 курс",  1);

    // --- Студенты ---
    studentService.AddStudent(1, "Иванов Иван",      "ivan@univ.ru",   "cs1");
    studentService.AddStudent(2, "Петрова Мария",     "maria@univ.ru",  "cs1");
    studentService.AddStudent(3, "Сидоров Алексей",   "alex@univ.ru",   "cs2");
    studentService.AddStudent(4, "Козлова Анна",      "anna@univ.ru",   "cs2");
    studentService.AddStudent(5, "Новиков Дмитрий",   "dmitry@univ.ru", "math1");
    studentService.AddStudent(6, "Морозова Елена",    "elena@univ.ru",  "math1");

    // --- Предметы ---
    gradeService.AddSubject("prog", "Программирование");
    gradeService.AddSubject("math", "Математика");
    gradeService.AddSubject("phys", "Физика");
    gradeService.AddSubject("eng",  "Английский язык");

    // --- Оценки ---
    var d = new DateTime(2025, 1, 15);

    gradeService.AddGrade(1, "prog", 5, d.AddDays(0),  GradeType.Exam);
    gradeService.AddGrade(1, "math", 4, d.AddDays(1),  GradeType.Exam);
    gradeService.AddGrade(1, "phys", 3, d.AddDays(2),  GradeType.Test);

    gradeService.AddGrade(2, "prog", 4, d.AddDays(3),  GradeType.Exam);
    gradeService.AddGrade(2, "math", 5, d.AddDays(4),  GradeType.Exam);
    gradeService.AddGrade(2, "eng",  5, d.AddDays(5),  GradeType.Homework);

    gradeService.AddGrade(3, "prog", 5, d.AddDays(6),  GradeType.Exam);
    gradeService.AddGrade(3, "math", 3, d.AddDays(7),  GradeType.Test);
    gradeService.AddGrade(3, "phys", 4, d.AddDays(8),  GradeType.CourseWork);

    gradeService.AddGrade(4, "prog", 3, d.AddDays(9),  GradeType.Exam);
    gradeService.AddGrade(4, "eng",  4, d.AddDays(10), GradeType.Test);
    gradeService.AddGrade(4, "math", 4, d.AddDays(11), GradeType.Homework);

    gradeService.AddGrade(5, "math", 5, d.AddDays(12), GradeType.Exam);
    gradeService.AddGrade(5, "phys", 5, d.AddDays(13), GradeType.Exam);
    gradeService.AddGrade(5, "prog", 4, d.AddDays(14), GradeType.Test);

    gradeService.AddGrade(6, "math", 4, d.AddDays(15), GradeType.Exam);
    gradeService.AddGrade(6, "eng",  3, d.AddDays(16), GradeType.Test);
    gradeService.AddGrade(6, "phys", 4, d.AddDays(17), GradeType.Homework);
}