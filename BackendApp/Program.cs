var app= WebApplication.CreateBuilder().Build();

app.MapGet("", () => "Hello HackYeah!");

app.Run();