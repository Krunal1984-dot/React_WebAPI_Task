using Store.BusinessLayer.Classes;
using Store.BusinessLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

builder.Services.AddScoped<IDbConnection>(conn => new SqlConnection(builder.Configuration["ConnectionString"]));
builder.Services.AddScoped(typeof(ILocationRepository), typeof(LocationRepository));

var app = builder.Build();

app.MapControllers();
app.Run();