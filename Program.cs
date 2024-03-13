using Microsoft.EntityFrameworkCore;
using Njal_back.Data;
using Njal_back.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<NjalDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("NjalDbConnectionString"))); var app = builder.Build();
builder.Services.AddDbContext<NjalDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NjalDbConnectionString")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
