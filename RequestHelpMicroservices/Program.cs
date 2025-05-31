using Microsoft.EntityFrameworkCore;
using RequestHelpMicroservices.Dcontext;
using RequestHelpMicroservices.RequestForHelp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmpReqContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRequestForHelpService, RequestForHelpService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", builder =>
        builder.WithOrigins("https://localhost:5163").AllowAnyHeader().AllowAnyMethod());
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
