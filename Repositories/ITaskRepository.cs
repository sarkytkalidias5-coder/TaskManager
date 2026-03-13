using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories;

public interface ITaskRepository
{
    IEnumerable<BaseTask> GetAll();
    BaseTask? GetById(Guid id);
    BugReportTask AddBugReport(string title, BugSeverityLevel severityLevel);
    FeatureRequestTask AddFeatureRequest(string title, int estimatedHours);
    bool Complete(Guid id);
}
