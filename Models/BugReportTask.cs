namespace TaskTrackerApi.Models;

public sealed class BugReportTask : BaseTask
{
    public BugSeverityLevel SeverityLevel { get; private set; }

    public BugReportTask(string title, BugSeverityLevel severityLevel) : base(title)
    {
        SeverityLevel = severityLevel;
    }
}
