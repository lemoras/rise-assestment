using Rise.Contact.API.Data;
using Rise.Contact.API.Helpers;
using Rise.Contact.API.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rise.Contact.API.Services;

var envname = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = envname
});

Console.WriteLine($"Env: {envname}");

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var connectionString = builder.Configuration.GetConnectionString(environment.EnvironmentName + "Connection");

if (environment.IsDevelopment())
    System.Console.WriteLine("dev is ok ");

if (environment.IsStaging())
    System.Console.WriteLine("stage is ok ");

if (environment.IsProduction())
    System.Console.WriteLine("production is ok ");

System.Console.WriteLine(environment.EnvironmentName + "Connection");

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseNpgsql(connectionString)
);

 // Default Policy
builder.Services.AddCors(options =>
{
     options.AddDefaultPolicy(
         builder =>
         {
             builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
         });
});

builder.Services.AddControllers();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IApplicationContext, ApplicationContext>();

builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

if (environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseHttpsRedirection(
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
// global cors policy
app.UseCors();
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseEndpoints(x => x.MapControllers());

if (environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ContactDbContext>();

        DataGenerator.Initialize(services);
    }
}

app.Run();

