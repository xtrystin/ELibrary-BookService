using ELibrary_BookService.Application.Command;
using ELibrary_BookService.Application.Query;
using ELibrary_BookService.Domain.Dapper;
using ELibrary_BookService.Domain.EF;
using ELibrary_BookService.Domain.EF.Repository;
using ELibrary_BookService.Domain.Repository;
using ELibrary_BookService.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
        opt.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddPostgres(builder.Configuration);

builder.Services.AddScoped<IDapperDataAccess, DapperDataAccess>();
builder.Services.AddScoped<IBookReadProvider, BookReadProvider>();
builder.Services.AddScoped<IBookProvider, BookProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() == false)
{
    app.UseExceptionHandler(c => c.Run(async context =>
    {
        var exception = context.Features
            .Get<IExceptionHandlerPathFeature>()
            .Error;
        var response = new { error = exception.Message };
        await context.Response.WriteAsJsonAsync(response);
    }));

}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
