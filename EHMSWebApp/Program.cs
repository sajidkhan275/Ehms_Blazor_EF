using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Serilog;
using EHMSWebApp.Microservices;
using Microsoft.AspNetCore.Http.Features;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient<EmployeeApiService>("HealthInfoService");
builder.Services.AddHttpClient<ReqForHelpApiServices>("HealthInfoService");
builder.Services.AddHttpClient<DepartmentApiService>("HealthInfoService");

builder.Services.AddHttpClient<EmployeePhysicalFitnessApiService>("HealthInfoService");
builder.Services.AddHttpClient<EmployeeHealthInfoApiService>("HealthInfoService");

builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("https://localhost:7197") });
builder.Services.AddHttpClient("HealthInfoService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7197"); 
});





//builder.Services.AddScoped<HttpClient>(sp =>
//{
// //   var uri = new Uri(builder.HostEnvironment.BaseAddress);  // Base URL for Ocelot
//    return new HttpClient { BaseAddress = uri };
//});

//  'https://localhost:7152/api/EmployeeHealthInfo'

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("1"));
    options.AddPolicy("HR", policy => policy.RequireRole("2"));
    options.AddPolicy("Employee", policy => policy.RequireRole("3"));
});

builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();


builder.Services.Configure(OpenIdConnectDefaults.AuthenticationScheme, (Action<OpenIdConnectOptions>)(options =>
{
    options.ClientId = builder.Configuration.GetSection("AzureAd").GetValue<string>("ClientId");
    options.Events.OnSignedOutCallbackRedirect = context =>
    {
        context.HttpContext.Response.Redirect(context.Properties?.RedirectUri ?? context.Options.SignedOutRedirectUri);
        context.HandleResponse();
        return Task.CompletedTask;
    };
}));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Logs to console
    .WriteTo.File(Path.Combine(logDirectory, "log.txt"), rollingInterval: RollingInterval.Day) 
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Host.UseSerilog();


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Xss-Protection"] = "1; mode=block";
    context.Response.Headers["Content-Security-Policy"] = "frame-ancestors 'self';";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    await next();
});
app.Run();
