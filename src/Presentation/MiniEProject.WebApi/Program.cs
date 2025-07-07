using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniEProject.Application.Helper;
using MiniEProject.Application.Mapping;
using MiniEProject.Application.Shared.Settings;
using MiniEProject.Application.Validations.CategoryValidations;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence;
using MiniEProject.Persistence.Contexts;
using MiniEProject.WebApi.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// ✅ FluentValidation (v11.6.0 və yuxarısı üçün)
builder.Services.AddValidatorsFromAssembly(typeof(CategoryCreateDtoValidator).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// ✅ Controller-lar və AutoMapper
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);

// ✅ DbContext
builder.Services.AddDbContext<MiniEProjectDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// ✅ Swagger + JWT Token Security
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter your JWT Token like this: Bearer {your token}",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// ✅ Custom Services (Repository, Service layer və s.)
builder.Services.RegisterService();
builder.Services.AddHttpContextAccessor(); // əgər istifadə edirsənsə

// ✅ Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<MiniEProjectDbContext>()
.AddDefaultTokenProviders();

// ✅ AppSettings - JWT və Email konfiqurasiya
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JWTSettings>();

// ✅ Authorization və Permissions
builder.Services.AddAuthorization(options =>
{
    foreach (var permission in PermissionHelper.GetPermissionList())
    {
        options.AddPolicy(permission, policy =>
        {
            policy.RequireClaim("Permission", permission);
        });
    }
});

// ✅ Authentication - JWT Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // production-da true et
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

// ✅ Build
var app = builder.Build();

// ✅ Middleware və HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

