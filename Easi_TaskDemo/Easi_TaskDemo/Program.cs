using Easi_TaskDemo.Configuration;
using Easi_TaskDemo.Mapper;
using Easy_Task.Common.Utilities;
using Easy_Task.Domain.Entities;
using Easy_Task.Persistence.Context;
using Easy_Task.Persistence.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddDependencies(configuration);
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddIdentityApiEndpoints<AppUser>().
    AddEntityFrameworkStores<EasyTaskDbContext>();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();
builder.Services.ConfigureAuthentication(configuration);




var app = builder.Build();

app.MapIdentityApi<AppUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
