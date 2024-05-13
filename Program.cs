using JWTAuthDotNet7;
using JWTAuthDotNet7.Feature.Login;
using JWTAuthDotNet7.Feature.Role;
using JWTAuthDotNet7.Feature.User;
using JWTAuthDotNet7.Helper;
//using JWTAuthDotNet7.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Db connection
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
}, ServiceLifetime.Transient, ServiceLifetime.Transient);

#endregion

#region Authentication and JwtBearer

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(opt =>
                 {
                     opt.SaveToken = true;
                     opt.RequireHttpsMetadata = false;
                     opt.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidIssuer = builder.Configuration["JWT:ValidateIssuer"],
                         ValidAudience = builder.Configuration["JWT:ValidateAudience"],
                         IssuerSigningKey = new SymmetricSecurityKey(
                                                    Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero,
                     };
                 });

#endregion

#region Injection

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<EncryptionService>();
builder.Services.AddScoped<AuthorizationHelper>();
//builder.Services.AddScoped<AuthenticationMiddleWare>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<AuthenticationMiddleWare>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
