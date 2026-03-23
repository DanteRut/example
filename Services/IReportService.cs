namespace UniversityApp.Services;

using UniversityApp.Dto;

public interface IReportService
{
    IEnumerable<SubjectStatResult> GetSubjectStats();
    IEnumerable<StudentAverageResult> GetExcellentStudents(double threshold);
    IEnumerable<GroupPerformanceResult> GetGroupPerformance();
}