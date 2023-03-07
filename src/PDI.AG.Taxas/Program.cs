using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using PDI.AG.Taxas.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adiciona todas as variaveis de ambiente as configura��es
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseHangFire();
builder.Services.UseRedis(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.UseIOCHangFire();

app.UseHangfireDashboard();

app.Run();

app.ScheduleTask();