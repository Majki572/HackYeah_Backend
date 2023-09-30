using BackendApp;
using Database;
using Database.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FridgeContext>();

var app= WebApplication.CreateBuilder().Build();
var Configuration = app.Configuration;

app.MapGet("", () => "Hello HackYeah!");

app.UseUserAPI();
app.UseFridgeAPI();
app.UseChatAPI();
app.UseGiveAwayAPI();

app.Run();