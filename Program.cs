using FeedbackApp.Api.Data;
using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NextJSPolicy"); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
