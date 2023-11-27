using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Mapping;
using StudentAPI.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI for POSTGRES
builder.Services.AddDbContext<StudentAPIDbContext>(options=>
    options.UseNpgsql(builder.Configuration.GetConnectionString("StudentAPIDb")) //app_db
);

builder.Services.AddDbContext<StudentAPIAuthDbContext>(options=>
    options.UseNpgsql(builder.Configuration.GetConnectionString("StudentAuthConnectionString"))
);

//DI Student Repositories
builder.Services.AddScoped<IStudentRepositories, PGSQLStudentsRepository>();

//DI newly created Token Repositories
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//Inject Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("StudentAPI")
    .AddEntityFrameworkStores<StudentAPIAuthDbContext>()
    .AddDefaultTokenProviders();

//setup Identity options to configure password settings
builder.Services.Configure<IdentityOptions>(Options=>
    {
        Options.Password.RequireDigit = true;
        Options.Password.RequireLowercase = false;
        Options.Password.RequireNonAlphanumeric = false;
        Options.Password.RequireUppercase = true;
        Options.Password.RequiredLength = 6;
        Options.Password.RequiredUniqueChars = 1;
    }
);


//Add Auth into Service collection
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer=builder.Configuration["Jwt:Issuer"],
        ValidAudience=builder.Configuration["Jwt:Audience"],
        IssuerSigningKey= new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
