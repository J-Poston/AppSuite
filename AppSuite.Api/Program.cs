using AppSuite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string appSuiteDb = builder.Configuration.GetConnectionString("AppSuiteDb");

builder.Services.AddDbContext<AppSuiteDbContext>(options => options.UseSqlServer(appSuiteDb, b => b.MigrationsAssembly("AppSuite.Api")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppSuiteDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Your Angular local URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    options.AddPolicy("AllowRender",
        policy =>
        {
            policy.WithOrigins("https://jposton-cmms.onrender.com") // Your Angular local URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}

if (app.Environment.IsProduction())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAngularDev");
app.UseCors("AllowRender");

app.MapIdentityApi<IdentityUser>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppSuiteDbContext>(); // Replace with your DbContext name

        // This checks your Migrations folder and automatically runs any pending updates on MonsterASP
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        //var logger = services.GetRequiredService<ILogger<Program>>();
        //logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();
