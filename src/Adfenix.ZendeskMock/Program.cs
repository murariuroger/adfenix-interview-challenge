using Adfenix.ZendeskMock.Extensions;
using Adfenix.ZendeskMock.Middlewares;
using Adfenix.ZendeskMock.Models;
using Adfenix.ZendeskMock.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();

var app = builder.Build();

app.UseMiddleware<BadSecurityMiddleware>();

app.MapGet("/", async ([FromServices] ICounterService counterService) => { 
    var counter = await counterService.GetCounterValueAsync();
    return new CountResponse { Count = counter };
});

app.Run("http://*:9000");