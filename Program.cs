using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Mapping;
using StudentAPI.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adding Serilog for logs and Global Exception handling
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/StudentAPI_Log.txt", rollingInterval: RollingInterval.Minute) //create logs per day
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); 

//apply auth into Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Student API", Version="v1"});

    //pass auth header to swagger
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme{
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    //add security scheme
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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


//Inject Middleware for global exception handling
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
