using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ScriptumLux.BLL;
using ScriptumLux.BLL.Interfaces;
using ScriptumLux.BLL.Services;
using ScriptumLux.DAL;

var builder = WebApplication.CreateBuilder(args);

// 1. Connection string and DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Data Source=application.db"));

// 2. AutoMapper configuration (scans BLL.MappingProfiles)
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

// 3. Register BLL services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IPlaylistMovieService, PlaylistMovieService>();
builder.Services.AddScoped<ITimecodeService, TimecodeService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 4. Add controllers
builder.Services.AddControllers();

// 5. Swagger for API documentation/testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScriptumLux API", Version = "v1" });
});

var app = builder.Build();

// 6. Ensure database is created and apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();  // or EnsureCreated()
}

// 7. Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScriptumLux API V1"));
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();