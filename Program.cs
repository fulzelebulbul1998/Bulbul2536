using COOKING_RECIPE_PORTAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<LoginContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));


builder.Services.AddControllers();

// Other services registration...

builder.Services.AddDbContext<LoginContext>();  /* configure your DB context */
builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
            
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {  
        builder.WithOrigins(
                            "http://localhost:7137","http://localhost:4200", "http://localhost:52402"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowAngularOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
