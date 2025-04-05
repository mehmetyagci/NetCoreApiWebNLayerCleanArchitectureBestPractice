using App.Repository;
using App.Repository.Extensions;
using App.Service;
using App.Service.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddControllers(options =>
{
    // FluentValidation filter'ını ekliyorsun (custom async validation için)
    options.Filters.Add<FluentValidationFilter>();
    // Non-nullable reference type'ların otomatik olarak required yapılmasını engeller (C# 8+ özellik)
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

// ASP.NET Core'un default model validation'ını iptal ediyorsun
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
}); // <- Otomatik validation iptal

// SWAGGER Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CUSTOM EXTENSIONS
builder.Services
    .AddRepositories(builder.Configuration)
    .AddServices(builder.Configuration);
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    // Eski kullanım şekli options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

//    var connectionString = builder.Configuration.
//    GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

//    options.UseSqlServer(connectionString!.SqlServer);
//}); 

var app = builder.Build();

app.UseExceptionHandler(x => { });

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