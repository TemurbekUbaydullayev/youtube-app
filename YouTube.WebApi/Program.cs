
// ---> Services
using Microsoft.EntityFrameworkCore;
using Serilog;
using YouTube.WebApi.Configurations;
using YouTube.WebApi.Data.DbContexts;
using YouTube.WebApi.Data.Interfaces;
using YouTube.WebApi.Data.Repositories;
using YouTube.WebApi.Service.Commons.Helpers;
using YouTube.WebApi.Service.Commons.Middlewares;
using YouTube.WebApi.Service.Interfaces;
using YouTube.WebApi.Service.Mappers;
using YouTube.WebApi.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//----> Corc Policy
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("AllowAll", accesses =>
        accesses.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddMemoryCache();

var connectionString = builder.Configuration.GetConnectionString("PostgresProductionDb");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//---> Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

//---> Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

//---> Mappers
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.ConfigureJwtAuthorize(builder.Configuration);
builder.Services.ConfigureSwaggerAuthorize(builder.Configuration);




// ---> Middleware
var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
