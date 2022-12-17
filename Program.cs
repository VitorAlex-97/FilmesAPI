using FilmesAPI.Data;
using FilmesAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FilmeService, FilmeService>();
builder.Services.AddScoped<CinemaService, CinemaService>();

builder.Services.AddDbContext<AppDbContext>(dbContextOptions  =>  { 
    dbContextOptions
    .UseLazyLoadingProxies()
    .UseMySql(
        builder.Configuration.GetConnectionString("FilmeConnection"), 
        new MySqlServerVersion(new Version(8,0)));
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

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
