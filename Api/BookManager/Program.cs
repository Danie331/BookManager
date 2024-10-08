using BookManager;
using BookManager.Middleware;
using ConfigurationDataProvider.Models;
using FluentValidation;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Supportwave assessment",
        Description = "RESTful API to manage a books inventory"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Fluent validation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// MediatR
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Automapper
builder.Services.AddAutoMapper(new Assembly[] { Assembly.GetAssembly(typeof(Program)) });

// Exception middleware
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

// Register config settings
builder.Services.Configure<BookDataStoreSettings>(builder.Configuration.GetSection("DataConfig"));

// Register services
ServiceRegistration.Register(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

    app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
