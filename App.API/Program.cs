using App.Repository.Extensions;
using App.Service;
using App.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>(); // FluentValidation filter'ını ekliyorsun (custom async validation için)
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // Non-nullable reference type'ların otomatik olarak required yapılmasını engeller (C# 8+ özellik)
});


// SWAGGER Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

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