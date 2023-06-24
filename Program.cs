using CustomerFeedback;
using CustomerFeedback.Context;
using CustomerFeedback.Endpoints.Account;
using CustomerFeedback.Endpoints.Feedback;
using CustomerFeedback.Extensions;
using CustomerFeedback.Models;
using CustomerFeedback.Repository;
using CustomerFeedback.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// get connection string dependent on environment
var connection = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");

// add services
builder.Services.AddAppServices(builder.Configuration, connection!);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();

// build app
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapFallbackToFile("index.html");

// API Endpoints
app.MapFeedbackEndpoints();
app.MapAccountEndpoints();

// create scope
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

// seed database
try
{
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager, roleManager);
}
catch (Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "An error occured");
}

app.Run();