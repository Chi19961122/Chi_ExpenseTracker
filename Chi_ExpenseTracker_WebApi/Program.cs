using Chi_ExpenseTracker_Repesitory.Configuration;
using Chi_ExpenseTracker_Repesitory.Infrastructure.Cors;
using Chi_ExpenseTracker_Repesitory.Infrastructure.Ioc;
using Chi_ExpenseTracker_Repesitory.Infrastructure.Jwt;
using Chi_ExpenseTracker_Repesitory.Infrastructure.Swagger;
using Chi_ExpenseTracker_Repesitory.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//實體化appsettings
builder.AddConfiguration(); 

//DB
builder.Services.AddDbContext<_ExpenseDbContext>(
        options => options.UseSqlServer(AppSettings.Connectionstrings?.ChiConn));
//套件
builder.AddSwagger();

builder.AddAutofac();

builder.AddJwt();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//其他
builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

builder.AddCorsPolicy();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.AddCorsConfigure();

app.MapControllers();

app.Run();
