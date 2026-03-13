using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Models;
using TaskTrackerApi.Repositories;
using TaskTrackerApi.Services;

namespace TaskTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TasksController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TasksController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BaseTask>> GetAll()
    {
        var tasks = _taskRepository.GetAll();
        return Ok(tasks);
    }

    [HttpGet("filtered")]
    public ActionResult<TaskFilterResult> GetFilteredData()
    {
        var result = TaskFilterService.FilterTasks(_taskRepository.GetAll());
        return Ok(result);
    }

    [HttpPost("bug")]
    public ActionResult<BugReportTask> CreateBugReport([FromBody] CreateBugReportRequest request)
    {
        var task = _taskRepository.AddBugReport(request.Title, request.SeverityLevel);
        return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
    }

    [HttpPost("feature")]
    public ActionResult<FeatureRequestTask> CreateFeatureRequest([FromBody] CreateFeatureRequestRequest request)
    {
        var task = _taskRepository.AddFeatureRequest(request.Title, request.EstimatedHours);
        return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
    }

    [HttpPut("{id:guid}/complete")]
    public IActionResult CompleteTask(Guid id)
    {
        var completed = _taskRepository.Complete(id);
        return completed ? NoContent() : NotFound();
    }
}

public sealed record CreateBugReportRequest(string Title, BugSeverityLevel SeverityLevel);
public sealed record CreateFeatureRequestRequest(string Title, int EstimatedHours);
