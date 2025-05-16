// File: ScriptumLux.API/Program.cs
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScriptumLux.API.Settings;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.BLL.Mappings;
using ScriptumLux.BLL.Services;
using ScriptumLux.DAL;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Load configuration files (appsettings.json, appsettings.Development.json)
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables();

// 2. Bind JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// 3. Add DbContext (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(conn);
});

// 4. AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

// 5. Register BLL services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IPlaylistMovieService, PlaylistMovieService>();
builder.Services.AddScoped<ITimecodeService, TimecodeService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();

// 6. Configure Authentication & JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    var key         = Encoding.UTF8.GetBytes(jwtOptions.Key);

    options.RequireHttpsMetadata = true;
    options.SaveToken            = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidIssuer              = jwtOptions.Issuer,
        ValidateAudience         = true,
        ValidAudience            = jwtOptions.Audience,
        ValidateLifetime         = true,
        IssuerSigningKey         = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization();

// 7. Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScriptumLux API", Version = "v1" });
});

var app = builder.Build();

// 8. Apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// 9. Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScriptumLux API V1"));
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
