# TaskTrackerApi

ASP.NET Core Web API microservice for the **Midterm Assignment: Task Management System**.

## Implemented requirements

### Block 1
- `BaseTask` abstract class with:
  - `Id` (`Guid`)
  - `Title` (`string`)
  - `CreatedAt` (`DateTime`)
  - `IsCompleted` (`bool`)
- `Id` and `CreatedAt` are protected by `init` accessors and assigned only during object creation.
- Derived classes:
  - `BugReportTask` with `SeverityLevel`
  - `FeatureRequestTask` with `EstimatedHours`
- Delegate + `OnTaskCompleted` event in `BaseTask`
- `CompleteTask()` updates status and raises the event
- `TaskFilterService` uses LINQ to:
  - get incomplete high-severity bug reports sorted from newest to oldest
  - calculate total estimated hours for incomplete feature requests

### Block 2
- `TasksController` endpoints:
  - `GET /api/tasks`
  - `GET /api/tasks/filtered`
  - `POST /api/tasks/bug`
  - `POST /api/tasks/feature`
  - `PUT /api/tasks/{id}/complete`
- DI is used to inject `ITaskRepository`
- In-memory repository implementation included

### Block 3
- See `IntegrationNotes.txt`
- Chosen pattern: **asynchronous integration with RabbitMQ**

### Docker
- Multi-stage `Dockerfile`
- Basic `docker-compose.yml`

## Run locally

```bash
dotnet restore
dotnet run
```

## Run with Docker

```bash
docker compose up --build
```

## Example requests

### Create bug report
```http
POST /api/tasks/bug
Content-Type: application/json

{
  "title": "API returns 500 on login",
  "severityLevel": 3
}
```

### Create feature request
```http
POST /api/tasks/feature
Content-Type: application/json

{
  "title": "Add dark mode",
  "estimatedHours": 12
}
```

### Complete task
```http
PUT /api/tasks/{id}/complete
```

## Suggested GitHub upload steps

```bash
git init
git add .
git commit -m "Add TaskTrackerApi midterm assignment"
git branch -M main
git remote add origin <YOUR_REPOSITORY_URL>
git push -u origin main
```
