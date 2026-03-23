// ==================== Dto/GroupPerformanceResult.cs ====================
namespace UniversityApp.Dto;

public class GroupPerformanceResult
{
    public string GroupName { get; set; } = "";
    public int StudentCount { get; set; }
    public double AverageGrade { get; set; }
}