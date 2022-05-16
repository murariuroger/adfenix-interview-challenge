using Adfenix.Visualiser.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run("http://*:8998");
