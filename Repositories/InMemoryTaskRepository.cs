using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories;

public sealed class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<BaseTask> _tasks = [];
    private readonly object _lock = new();

    public InMemoryTaskRepository()
    {
        Seed();
    }

    public IEnumerable<BaseTask> GetAll()
    {
        lock (_lock)
        {
            return _tasks.ToList();
        }
    }

    public BaseTask? GetById(Guid id)
    {
        lock (_lock)
        {
            return _tasks.FirstOrDefault(task => task.Id == id);
        }
    }

    public BugReportTask AddBugReport(string title, BugSeverityLevel severityLevel)
    {
        var task = new BugReportTask(title, severityLevel);
        SubscribeToCompletionEvent(task);

        lock (_lock)
        {
            _tasks.Add(task);
        }

        return task;
    }

    public FeatureRequestTask AddFeatureRequest(string title, int estimatedHours)
    {
        var task = new FeatureRequestTask(title, estimatedHours);
        SubscribeToCompletionEvent(task);

        lock (_lock)
        {
            _tasks.Add(task);
        }

        return task;
    }

    public bool Complete(Guid id)
    {
        lock (_lock)
        {
            var task = _tasks.FirstOrDefault(task => task.Id == id);
            if (task is null)
            {
                return false;
            }

            task.CompleteTask();
            return true;
        }
    }

    private void Seed()
    {
        AddBugReport("Fix login 500 error", BugSeverityLevel.Critical);
        AddBugReport("Resolve dashboard alignment issue", BugSeverityLevel.Medium);
        AddFeatureRequest("Add task comments", 8);
        AddFeatureRequest("Implement export to CSV", 5);
    }

    private static void SubscribeToCompletionEvent(BaseTask task)
    {
        task.OnTaskCompleted += completedTask =>
        {
            Console.WriteLine($"Task completed: {completedTask.Title} ({completedTask.Id}) at {DateTime.UtcNow:O}");
        };
    }
}
