using CustomerFeedback;
using CustomerFeedback.Context;
using CustomerFeedback.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

// add swagger
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
        }
    );
    x.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        }
    );
});

// add fluent validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Feedback>();

// add Authentication with JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

// add authorization policy
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

// get connection string dependent on environment
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

// don't map controllers
// app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

// api endpoints
app.MapGet("/api/hello", () => "Hello World!");

// feedback endpoitns
// don't have to be signed in to get feedback
app.MapGet(
        "/api/feedback",
        async (AppDbContext context) =>
        {
            return await context.Feedbacks.ToListAsync();
        }
    )
    .AllowAnonymous()
    .Produces<List<Feedback>>(StatusCodes.Status200OK);

app.MapPost(
        "/api/feedback/",
        async (AppDbContext context, IValidator<Feedback> validator, Feedback feedback) =>
        {
            var validationResult = await validator.ValidateAsync(feedback);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
                return Results.BadRequest(errors);
            }

            await context.Feedbacks.AddAsync(feedback);
            await context.SaveChangesAsync();
            return Results.Ok();
        }
    )
    .AllowAnonymous();

// .Produces(StatusCodes.Status201Created);

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

            return Results.Ok();
        }
    )
    .AllowAnonymous()
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
    .Produces(StatusCodes.Status204NoContent)
    .AllowAnonymous();

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


// builder.Services
//     .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.Authority =
//             builder.Configuration["AzureAd:Instance"] + builder.Configuration["AzureAd:TenantId"];
//         options.Audience = builder.Configuration["AzureAd:ClientId"];
//     });
