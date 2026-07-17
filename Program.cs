using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.Services;
using SeatsReservationDotNet.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var dbConfig = builder.Configuration.GetSection("Database");
var csBuilder = new NpgsqlConnectionStringBuilder
{
    Host = dbConfig["Host"],
    Port = int.Parse(dbConfig["Port"]),
    Database = dbConfig["Name"],
    Username = dbConfig["Username"],
    Password = dbConfig["Password"],
    SearchPath = "base_schema",
};

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(csBuilder.ConnectionString));

// Mapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
}, typeof(Program).Assembly);

builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IPriceCategoryService, PriceCategoryService>();
builder.Services.AddScoped<ISessionSeatService, SessionSeatService>();

// Auth services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token:"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            []
        },
    });
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero, // no grace period on token expiry
        };
    });

builder.Services.AddAuthorization();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://+:{port}");

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var (status, message) = error switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, error.Message),
            InvalidOperationException => (StatusCodes.Status400BadRequest, error.Message),
            _ => (StatusCodes.Status500InternalServerError, "An internal error occurred"),
        };
        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new { error = message });
    });
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
