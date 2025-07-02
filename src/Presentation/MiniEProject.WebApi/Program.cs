using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence;
using MiniEProject.Persistence.Contexts;
using MiniEProject.Persistence.Repositories;
using MiniEProject.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MiniEProjectDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.RegisterService();

builder.Services.AddIdentity<AppUser, IdentityRole>(
options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<MiniEProjectDbContext>()
    .AddDefaultTokenProviders();


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
