using Profolio.Server.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.CustomService(builder.Configuration, builder.Environment);

var app = builder.Build();
app.CustomMiddleware(app, app.Environment, app.Configuration, app.Logger);
app.Run();