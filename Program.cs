using CustomerFeedback;
using CustomerFeedback.Context;
using CustomerFeedback.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connection, options => options.EnableRetryOnFailure())
);

// build app and add api endpoints, configure, add middleware
var app = builder.Build();

// Configure
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

// app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

// api endpoints
app.MapGet("/api/hello", () => "Hello World!");

// feedback endpoitns
app.MapGet(
        "/api/feedback",
        async (AppDbContext context) =>
        {
            return await context.Feedbacks.ToListAsync();
        }
    )
    .Produces<List<Feedback>>(StatusCodes.Status200OK);

app.MapPost(
        "/api/feedback",
        async (AppDbContext context, Feedback feedback) =>
        {
            await context.Feedbacks.AddAsync(feedback);
            await context.SaveChangesAsync();
            return Results.CreatedAtRoute();
        }
    )
    .Produces(StatusCodes.Status201Created);

app.MapPut(
        "/api/feedback/{id}",
        async (AppDbContext context, int id, Feedback newFeedback) =>
        {
            var feedback = await context.FindAsync<Feedback>(id);
            if (feedback == null)
                return Results.NotFound();
            feedback.Title = newFeedback.Title;
            feedback.Description = newFeedback.Description;
            feedback.Rating = newFeedback.Rating;
            feedback.DateReviewed = newFeedback.DateReviewed;
            await context.SaveChangesAsync();

            return Results.CreatedAtRoute();
        }
    )
    .Produces(StatusCodes.Status201Created);

app.MapDelete(
        "/api/feedback/{id}",
        async (AppDbContext context, int id) =>
        {
            var feedback = await context.FindAsync<Feedback>(id);
            if (feedback == null)
                return Results.NotFound();
            context.Remove(feedback);
            await context.SaveChangesAsync();
            return Results.Ok(204);
        }
    )
    .Produces(StatusCodes.Status204NoContent);

// seed database
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (System.Exception)
{
    throw;
}

app.Run();
