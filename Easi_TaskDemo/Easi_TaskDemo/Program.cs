using Easi_TaskDemo.Mapper;
using Easy_Task.Domain.Entities;
using Easy_Task.Persistence.Context;
using Easy_Task.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddDependencies(configuration);
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<EasyTaskDbContext>()
               .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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

app.Run();
