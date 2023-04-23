using EntityMessanger.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<EntityMessangerHub>("/entitymessanger");
app.Run();
