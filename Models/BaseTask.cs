namespace TaskTrackerApi.Models;

public abstract class BaseTask
{
    public delegate void TaskCompletedHandler(BaseTask task);
    public event TaskCompletedHandler? OnTaskCompleted;

    public Guid Id { get; init; }
    public string Title { get; private set; }
    public DateTime CreatedAt { get; init; }
    public bool IsCompleted { get; private set; }

    protected BaseTask(string title)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Title = title;
        IsCompleted = false;
    }

    public void CompleteTask()
    {
        if (IsCompleted)
        {
            return;
        }

        IsCompleted = true;
        OnTaskCompleted?.Invoke(this);
    }
}
