using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaApi.Entities;
using SocialMediaApi.Migrations;
using SocialMediaApi.Repositories;
using SocialMediaApi.Repositories.Interfaces;
using SocialMediaApi.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentityCore<UserEntity>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityApiEndpoints<UserEntity>().AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 6;
});

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes("%SocialMediaSecret@1183002#Vietnamese%");

        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token in this field",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(
            policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapIdentityApi<UserEntity>();

app.MapControllers();

app.Run();


