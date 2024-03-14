using Microsoft.EntityFrameworkCore;
using Njal_back.Data;
using Njal_back.Helpers;
using Njal_back.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// this line adds newtonsoftJson for patching which i am not using currently
//builder.Services.AddControllers().AddNewtonsoftJson();

// Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// this line adds connection to SQL
builder.Services.AddDbContext<NjalDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NjalDbConnectionString")));
// this line adds Unitofwork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add Cors policy
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
