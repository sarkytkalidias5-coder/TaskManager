namespace TaskTrackerApi.Models;

public sealed record TaskFilterResult(
    IReadOnlyList<BugReportTask> HighSeverityIncompleteBugReports,
    int TotalEstimatedHoursForIncompleteFeatureRequests);
