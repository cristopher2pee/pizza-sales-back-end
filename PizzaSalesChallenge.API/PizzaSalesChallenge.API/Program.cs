using Microsoft.EntityFrameworkCore;
using PizzaSalesChallenge.Infrastructure.Database;
using PizzaSalesChallenge.Business.Extension;
using PizzaSalesChallenge.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PizzaDatabaseContext>(
    options => options.UseSqlServer(builder.Configuration["Data:ConnectionString:DefaultConnection"]));

builder.Services.PizzaSalesChallengeBusinessExtension();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomHandlerException>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
