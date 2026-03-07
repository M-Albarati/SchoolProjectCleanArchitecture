using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using SchoolProject.Core;
using SchoolProject.Core.Middleware;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.Repositaries;
using SchoolProject.Infrustructure.Seeder;
using SchoolProject.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Connection To Sql Server
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});
#endregion

#region Dependency Injection
//builder.Services.AddTransient<IStudentRepositary, StudentRepositary>();
builder.Services.AddInfrustructureDependencies()
                .AddServiceDependencies()
                .AddCoreDependencies()
                .AddServiceRegisteration(builder.Configuration);
#endregion

#region AllowCORS "Cross Origin" 
var CORS = "_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          /* 
                           // Allow Specific Origion
                           policy.WithOrigins("https://MyServer1IP", "https://MyServer2IP");
                          */
                          // Allow Any Origion
                          policy.AllowAnyOrigin();
                      });

});
#endregion

var app = builder.Build();

// Seed Initial Roles And Admin User
using (var scope = app.Services.CreateScope())
{
    var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    await RoleSeeder.SeedAsync(_roleManager);
    await UserSeeder.SeedAsync(_userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

//AllowCORS "Cross Origin"
app.UseCors(CORS);

// Add Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
