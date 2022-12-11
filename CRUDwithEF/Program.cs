using CRUDwithEF.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DbContext service
/*
 * using 'InMemory' for testing purposes.
 * 
 * Convert it to real MSSQL Server
 * 1) Download SqlServer
 * 2) Tools
 * 3) Create the connection string
 * 4) replace the: opt.UseInMemoryDatabase("ContactDB") ==> opt.UseSqlServer(builder.Configuration.GetConnectionString("ContactDB")
 * 5) Add-Migration
 * 6) Update-Database
 */
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("ContactDB"));


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
