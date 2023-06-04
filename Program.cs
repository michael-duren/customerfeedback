using CustomerFeedback;
using CustomerFeedback.Context;
using CustomerFeedback.EndpointDefinitions;
using CustomerFeedback.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// get connection string dependent on environment
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
else
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");

// add services
builder.Services.AddAppServices(builder.Configuration, connection!);
builder.Services.AddIdentityServices(builder.Configuration);

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

// feedback endpoitns
FeedbackEndpoint.Map(app);

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
