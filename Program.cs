using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Mapping;
using StudentAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI for POSTGRES
builder.Services.AddDbContext<StudentAPIDbContext>(options=>
    options.UseNpgsql(builder.Configuration.GetConnectionString("StudentAPIDb"))
);

//Repo Pattern
builder.Services.AddScoped<IStudentRepositories, PGSQLStudentsRepository>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
