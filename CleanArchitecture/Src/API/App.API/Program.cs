using App.API.ExceptionHandler;
using App.API.Filters;
using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Domain.Exceptions;
using App.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters
        .Add<FluentValidationFilter>(); // FluentValidation filter'ını ekliyorsun (custom async validation için)
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes =
        true; // Non-nullable reference type'ların otomatik olarak required yapılmasını engeller (C# 8+ özellik)
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);
builder.Services.AddScoped(typeof(NotFoundFilter<,>));
builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, CacheService>();

var app = builder.Build();

app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();