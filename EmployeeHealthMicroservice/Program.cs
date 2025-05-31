using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Application.Services;
using EmployeeHealthMicroservice.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmployeeHealthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeHealthInfoService, EmployeeHealthInfoService>();
//builder.Services.AddScoped<IEmployeeHealthInfoService, EmployeeHealthInfoService1>();
builder.Services.AddScoped<IEmployeePhysicalFitnessService, EmployeePhysicalFitnessService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeHealthInfoService, EmployeeHealthInfoService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", builder =>
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        //builder.WithOrigins("https://localhost:5163").AllowAnyHeader().AllowAnyMethod());
});

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
app.UseCors("AllowBlazorApp");
app.Run();
