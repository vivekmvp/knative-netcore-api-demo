using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using TodoApiLatest.Extensions;
using TodoApiLatest.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(4, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersionedApiExplorer(c =>
{
    c.GroupNameFormat = "'v'VVV";
    c.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c =>
{    
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API v1.0"        
    });
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "ToDo API v2.0"
    });
    c.SwaggerDoc("v3", new OpenApiInfo
    {
        Version = "v3",
        Title = "ToDo API v3.0"
    });
    c.SwaggerDoc("v4", new OpenApiInfo
    {
        Version = "v4",
        Title = "ToDo API v4.0"
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//For In-Memory Db Operations
builder.Services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //*******************
    //Put here so that swagger will be available in development
    //And it's unavailable when you deployed to Azure
    //app.UseSwaggerWithVersioning();
    //*******************
}

//Putting this here for testing purpose only
app.UseSwaggerWithVersioning();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
