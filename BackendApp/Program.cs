using BackendApp;
using BackendApp.ChatLogic.BusinessLogic;
using Database;
using Database.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>();

// builder.Services.AddTransient<IChatLogic, ChatLogicService>(provider => new ChatLogicService(provider.GetService()));

var app= WebApplication.CreateBuilder().Build();
var Configuration = app.Configuration;

app.MapGet("", () => "Hello HackYeah!");

app.UseUserAPI();
app.UseFridgeAPI();
app.UseChatAPI();
app.UseGiveAwayAPI();

app.Run();