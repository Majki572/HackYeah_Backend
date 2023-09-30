using BackendApp;

var app= WebApplication.CreateBuilder().Build();

app.MapGet("", () => "Hello HackYeah!");

app.UseUserAPI();
app.UseFridgeAPI();
app.UseChatAPI();
app.UseGiveAwayAPI();

app.Run();