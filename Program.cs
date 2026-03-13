using TaskTrackerApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dias Task Tracker API v1");
    options.DocumentTitle = "Dias Task Tracker API";
});

// Главная страница
app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Dias Task Tracker API</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background: linear-gradient(135deg, #0f172a, #1e293b);
            color: white;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .card {
            width: 700px;
            max-width: 90%;
            background: rgba(30, 41, 59, 0.95);
            border-radius: 20px;
            padding: 40px;
            box-shadow: 0 0 30px rgba(0,0,0,0.35);
            text-align: center;
        }

        h1 {
            margin-bottom: 15px;
            font-size: 36px;
        }

        p {
            font-size: 18px;
            line-height: 1.6;
            opacity: 0.9;
        }

        .features {
            text-align: left;
            margin: 25px auto;
            display: inline-block;
            font-size: 17px;
            line-height: 1.8;
        }

        a.button {
            display: inline-block;
            margin-top: 20px;
            padding: 14px 28px;
            background: #38bdf8;
            color: #0f172a;
            text-decoration: none;
            font-weight: bold;
            border-radius: 12px;
            transition: 0.2s ease;
        }

        a.button:hover {
            background: #0ea5e9;
            transform: scale(1.03);
        }

        .footer {
            margin-top: 25px;
            font-size: 14px;
            opacity: 0.7;
        }
    </style>
</head>
<body>
    <div class="card">
        <h1>Dias Task Tracker API</h1>
        <p>Midterm project built with ASP.NET Core Web API.</p>

        <div class="features">
            ✔ Inheritance-based task models<br>
            ✔ Event triggered when task is completed<br>
            ✔ Repository pattern implementation<br>
            ✔ Docker container support<br>
            ✔ Imba voobshem 
        </div>

        <br>

        <a class="button" href="/swagger">Open Swagger Documentation</a>

        <div class="footer">
            Created by Dias
        </div>
    </div>
</body>
</html>
""", "text/html"));

app.MapControllers();

app.Run();