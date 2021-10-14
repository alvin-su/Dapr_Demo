using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();

builder.WebHost.UseUrls("http://*:6600");


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.Use((context, next) =>
//{
//    context.Request.EnableBuffering();
//    return next();
//});

app.UseRouting();

app.UseCloudEvents();

app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
    endpoints.MapControllers();
});

app.Run();
