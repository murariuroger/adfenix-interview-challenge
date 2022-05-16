using Adfenix.CampaignServersMock.Extensions;
using Adfenix.CampaignServersMock.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();

var app = builder.Build();

app.MapGet("/count", async (HttpRequest request, [FromServices] ICounterService counterService) =>
{
    var hostName = request.Host.Value;
    var server = hostName.Split(".")[0];
    var serverCounter = await counterService.GetCounterValueAsync(server);

    return $"new count: {serverCounter}";
});

app.Run("http://*:8999");