using ELibrary_BookService.Application;
using ELibrary_BookService.Domain.Dapper;
using ELibrary_BookService.Domain.EF;
using ELibrary_BookService.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IDapperDataAccess, DapperDataAccess>();
builder.Services.AddScoped<IBookReadProvider, BookReadProvider>();
builder.Services.AddScoped<IBookProvider, BookProvider>();

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
