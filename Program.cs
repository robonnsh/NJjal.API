using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Njal_back.Data;
using Njal_back.Helpers;
using Njal_back.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


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
// jwt authentication
var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;
var key = new SymmetricSecurityKey(Encoding.UTF32
  .GetBytes(secretKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,  
                IssuerSigningKey = key
            };
        });

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Authentication
app.UseAuthentication();

// Add Cors policy
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
