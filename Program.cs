using CustomerFeedback.Context;
using CustomerFeedback.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
	connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
	connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connection)
);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/feedback", async (AppDbContext context) =>
{
	var feedbackList = await context.Feedbacks.ToListAsync();
	return Results.Ok(feedbackList);
})
.WithName("GetFeedbacks");

app.MapPost("/feedback", async (Feedback feedback, AppDbContext context) =>
{
	await context.Feedbacks.AddAsync(feedback);
	await context.SaveChangesAsync();
	return Results.Ok(feedback);
})
.WithName("AddFeedback");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
