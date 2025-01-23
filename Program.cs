using Asp.Versioning;
using Eventei_Api.Models.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
try
{
    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);  // Automatically detect MySQL version
    Console.WriteLine("MySQL server version detected successfully.");

    builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseMySql(connectionString, serverVersion, mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5, // Retry up to 5 times
                maxRetryDelay: TimeSpan.FromSeconds(10), // Max delay between retries
                errorNumbersToAdd: null // Specify error codes to add to the retry list
            );
        })
    );
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred while detecting the MySQL version: {ex.Message}");
    throw;
}