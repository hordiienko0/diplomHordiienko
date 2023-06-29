using System.Diagnostics;
using Ctor;
using Ctor.Application;
using Ctor.Hubs;
using Ctor.Infrastructure;
using Ctor.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

// Initialise and seed database
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    await initialiser.InitialiseAsync();

    if(app.Environment.IsDevelopment())
        await initialiser.SeedAsync();
}

app.UseHealthChecks("/health");

string[] origins = {"http://localhost:4200", "https://thankful-sand-0d354de03.1.azurestaticapps.net"};

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins(origins)   
);




app.UseHttpsRedirection();

app.UseRouting();

var filesFolderPath = Path.GetFullPath(builder.Configuration["FilesFolder"]);

if(!Directory.Exists(filesFolderPath))
    Directory.CreateDirectory(filesFolderPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(filesFolderPath ?? ""),

    RequestPath = "/files",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
            "Origin, X-Requested-With, Content-Type, Accept");
    },
});
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationsHub>("/notification");
});

app.MapControllers();
app.ConfigureBus();

app.Run();

public partial class Program
{
}
