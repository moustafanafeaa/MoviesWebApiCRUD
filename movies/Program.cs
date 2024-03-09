using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using movies;
using movies.Filters;
using movies.MiddleWares;
using movies.Models;
using movies.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("cinfig.json");
builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments"));

///\
///Add services to the container.

///type of db and name of conStr
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(connectionString)
    );
builder.Services.AddControllers(options=> {
    options.Filters.Add<LogActivityFilter>();
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
//
builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<IMoviesService, MoviesService>();
builder.Services.AddAutoMapper(typeof(Program));
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RateLimitingMiddleware>();
app.UseHttpsRedirection();

app.UseMiddleware<ProfilingMiddleWare>();

//to allow many app in other port number to read data from this api 
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();


app.MapControllers();

app.Run();
