using M.Blog.BLL.Extensions;
using M.Blog.DAL;
using M.Blog.BLL.Extensions;
using M.Blog.BLL.Services;
using System.ComponentModel;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddDataAccess();
builder.Services.AddBusinessLogic(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo()
        {
            Title = "My API - V1",
            Version = "v1"
        }
    );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "api1.xml");
    c.IncludeXmlComments(filePath);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
