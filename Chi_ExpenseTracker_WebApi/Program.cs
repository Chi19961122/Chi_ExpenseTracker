using Chi_ExpenseTracker_Repesitory.Configuration;
using Chi_ExpenseTracker_Repesitory.Database;
using Chi_ExpenseTracker_Repesitory.Database.Repository;
using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Common.Jwt;
using Chi_ExpenseTracker_Service.Common.User;
using Chi_ExpenseTracker_Service.Common.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//實體化appsettings
builder.AddConfiguration(); 
//DB
builder.Services.AddDbContext<_ExpenseDbContext>(
        options => options.UseSqlServer(AppSettings.Connectionstrings?.Company));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//依賴注入
builder.Services.AddScoped(typeof(DbBase<,>));
builder.Services.AddScoped<IJwtAuthService, JwtAuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//其他
builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();



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
