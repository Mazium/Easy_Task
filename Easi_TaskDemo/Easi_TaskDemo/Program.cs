using Easi_TaskDemo.Configuration;
using Easi_TaskDemo.Mapper;
using Easy_Task.Application.Hubs;
using Easy_Task.Common.Utilities;
using Easy_Task.Persistence.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDependencies(configuration);
builder.Services.ConfigureAuthentication(configuration);
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddSignalR();

// Configure Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await Seeder.SeedRolesAndSuperAdmin(serviceProvider);
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Map the SignalR hub
app.MapHub<StreamingHub>("/streaming-hub");

app.Run();
