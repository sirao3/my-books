using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data;
using my_books;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddTransient<BooksService>();
builder.Services.AddTransient<PublishersService>();
builder.Services.AddTransient<AuthorsService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//AppDbInitialiser.Seed(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.ConfigureBuildInExceptionHandler();
//app.ConfigureCustomExceptionHandler();

app.MapControllers();

app.Run();


