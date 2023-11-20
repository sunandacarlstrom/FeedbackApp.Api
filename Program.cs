using FeedbackApp.Api.Models;
using FeedbackApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<MongoDBSettings>(); 

// ...bind our settings as a MongoDBService 
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB")); 
builder.Services.AddSingleton<MongoDBService>();  

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
