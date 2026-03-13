using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services;

public static class TaskFilterService
{
    public static TaskFilterResult FilterTasks(IEnumerable<BaseTask> tasks)
    {
        var highSeverityIncompleteBugReports = tasks
            .OfType<BugReportTask>()
            .Where(task => !task.IsCompleted && task.SeverityLevel >= BugSeverityLevel.High)
            .OrderByDescending(task => task.CreatedAt)
            .ToList();

        var totalEstimatedHoursForIncompleteFeatureRequests = tasks
            .OfType<FeatureRequestTask>()
            .Where(task => !task.IsCompleted)
            .Sum(task => task.EstimatedHours);

        return new TaskFilterResult(
            highSeverityIncompleteBugReports,
            totalEstimatedHoursForIncompleteFeatureRequests);
    }
}
