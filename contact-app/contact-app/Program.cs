using contact_app.AppDbContext;
using contact_app.Interface;
using contact_app.Service;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
// Register the DbContext
builder.Services.AddDbContext<ContactsAPIDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
           .LogTo(Console.WriteLine); // Add logging to console for debugging
});

// Register the ContactService
builder.Services.AddScoped<ContactService>();
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add services to the container.

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
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
