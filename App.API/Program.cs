using App.Repository;
using App.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories(builder.Configuration);
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    // Eski kullanım şekli options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

//    var connectionString = builder.Configuration.
//    GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

//    options.UseSqlServer(connectionString!.SqlServer);
//}); 

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
