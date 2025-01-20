using ASPMVC.MySQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("Connection string 'MySqlConnection' not found.");
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<SqlService, SqlService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    // Allow the pipeline to process the request first
    await next();

    // If the request starts with /api and wasn't handled, return a 404 error
    if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\":\"API endpoint not found\"}");
        return;
    }

    // For non-API routes, serve the Angular index.html if the response is 404 and no static file was found
    if (context.Response.StatusCode == StatusCodes.Status404NotFound &&
        !context.Request.Path.StartsWithSegments("/api"))
    {
        context.Response.StatusCode = StatusCodes.Status200OK;
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync("wwwroot/index.html");
    }
});


app.Run();
