var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Support for controllers (MVC style routing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Resolve http -> https redirection issues.
    // If you change ports in launchSettings.json, ensure they match here.
    serverOptions.ListenLocalhost(5155); // HTTP port.
    serverOptions.ListenLocalhost(7295, listenOptions =>
    {
        listenOptions.UseHttps();
    }); // HTTPS port.

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS

app.UseAuthorization(); // Enables authorization middleware

app.MapControllers(); // Maps HTTP endpoints to controllers

app.MapGet("/", ctx =>
{
    ctx.Response.Redirect("/swagger/index.html"); // Redirects root to Swagger UI.
    return Task.CompletedTask;
});

app.Run();
