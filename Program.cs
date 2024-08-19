using BacklogAPI.Data;
using Microsoft.EntityFrameworkCore;
using BacklogAPI.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add HTTPClient DI
builder.Services.AddHttpClient();

builder.Services.AddDbContext<BacklogDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped<GameRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();