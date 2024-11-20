using System.Threading;
using ValyutaBot.Handlers;
using ValyutaBot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var token = builder.Configuration["BotConfiguration:Token"];
///////////////////////////////
/////////////////////////////////
///////////////////////////////
///TOKEN YOZISH KERAK appsettings.json FAYLIGA 
///API DASTURCHILAR UCHUN VALYUTA API SAYTIDAN OLINGAN

builder.Services.AddScoped<HandleUpdate>();
builder.Services.AddScoped<BotService>(b => new BotService(token, b.GetRequiredService<HandleUpdate>()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var botService = scope.ServiceProvider.GetService<BotService>();
    var cts = new CancellationTokenSource();
    await botService.StartReceiving(cts.Token);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();




///////////////////////////////
/////////////////////////////////
///////////////////////////////
///TOKEN YOZISH KERAK appsettings.json FAYLIGA
///API DASTURCHILAR UCHUN VALYUTA API SAYTIDAN OLINGAN