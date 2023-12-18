using System.Text;
using System.Text.Json.Serialization;
using FeedbackApp.Api.Data;
using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.IO;

var builder = WebApplication.CreateBuilder(args);



// Connect to MongoDB
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<FeedbackAppContext>();

// Add services to the container.
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<CompanyService>();
builder.Services.AddSingleton<EventService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NextJSPolicy", builder =>
    {
        builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, Role>()
    .AddMongoDbStores<User, Role, ObjectId>
    (
        builder.Configuration.GetSection("MongoDB")["ConnectionURI"],
        builder.Configuration.GetSection("MongoDB")["DatabaseName"]
    )
    .AddDefaultTokenProviders();

// Set up authentication...
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

if (jwtSettings == null)
{
    throw new Exception("JWT settings missing in User Secrets");
}

builder.Services.AddSingleton(jwtSettings);

var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,

        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Seed the database...
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<FeedbackAppContext>();
var userService = services.GetRequiredService<UserService>();
var userManager = services.GetRequiredService<UserManager<User>>();
var roleManager = services.GetRequiredService<RoleManager<Role>>();

await SeedData.LoadRoles(roleManager);

// Get admin settings for use in SeedData
var adminSettings = builder.Configuration
    .GetSection(nameof(AdminSettings))
    .Get<AdminSettings>();

await SeedData.LoadUserData(userService, userManager, adminSettings);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NextJSPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
