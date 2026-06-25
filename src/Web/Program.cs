using Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CONTROLLERS
builder.Services.AddControllers();


// OPENAPI / SWAGGER 
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        (document, context, cancellationToken) =>
        {
            var schemeName = "ApiBearerAuth";

            var securityScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description =
                    "Paste the JWT token here."
            };

            document.Components ??= new OpenApiComponents();

            document.Components.SecuritySchemes
                ??= new Dictionary<string, IOpenApiSecurityScheme>();

            document.Components.SecuritySchemes[schemeName]
                = securityScheme;

            var schemeReference =
                new OpenApiSecuritySchemeReference(
                    schemeName,
                    document
                );

            var requirement =
                new OpenApiSecurityRequirement
                {
                    [schemeReference] = []
                };

            document.Security =
                new List<OpenApiSecurityRequirement>
                {
                    requirement
                };

            return Task.CompletedTask;
        });
});



// INYECCION DE DEPENDENCIAS 


//CLOUDINARY
builder.Services.AddHttpClient(
    "Cloudinary",
    client =>
    {
        var cloudName =
            builder.Configuration["CloudinarySettings:CloudName"];

        client.BaseAddress =
            new Uri(
                $"https://api.cloudinary.com/v1_1/{cloudName}/"
            );
    });

builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

// AUTH
builder.Services.AddScoped<ICustomAuthenticationService, AuthenticationService>();


// COMPANY
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// USER
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// CV
builder.Services.AddScoped<ICvRepository, CvRepository>();
builder.Services.AddScoped<ICvService, CvService>();

// NOTIFICATION
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// EXPERIENCE
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();

// EDUCATION
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEducationService, EducationService>();

// MIDDLEWARE
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();


// CONFIG DE LA BDD 
string connectionString = builder.Configuration["ConnectionStrings:SQLiteConnectionString"]!;
var connection = new SqliteConnection(connectionString);
connection.Open();

using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE;";
    command.ExecuteNonQuery();
}

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(connection));


// JWT AUTHENTICATION
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Authentication:Issuer"],

                ValidAudience = builder.Configuration["Authentication:Audience"],

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Authentication:SecretForKey"]!
                    )
                )
            };
    });



// APP


var app = builder.Build();

#region Apply EF Migrations

using (var serviceScopescope = app.Services.CreateScope())
{
    var dbContext = serviceScopescope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dbContext.Database.Migrate();
}

#endregion

// GLOBAL EXCEPTION MIDDLEWARE
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// SWAGGER
//if (app.Environment.IsDevelopment())
//{
app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/openapi/v1.json",
        "JobLink API V1"
    );


});
//}

// PIPELINE
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


// RUN
app.Run();