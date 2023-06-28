using ELibrary_BookService.Application;
using ELibrary_BookService.Extensions;
using ELibrary_BookService.Infrastructure.EF;
using ELibrary_BookService.RabbitMq;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
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
builder.Services.AddServiceBus(builder.Configuration);

builder.Services.AddProviderCollection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Configuration["Flags:EnableUserFriendlyErrorMessages"] == "1")
{
    app.UseExceptionHandler(c => c.Run(async context =>
    {
        var exception = context.Features
            .Get<IExceptionHandlerPathFeature>()
            .Error;
        //var response = new { error = exception.Message };
        var response = exception.Message;
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(response);
    }));

}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseMetricServer();
app.UseHttpMetrics(options => options.AddCustomLabel("host", context => context.Request.Host.Host));

app.MapControllers();

app.Run();
