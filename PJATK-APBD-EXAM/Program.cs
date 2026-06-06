using Microsoft.EntityFrameworkCore;
using PJATK_APBD_EXAM.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// dotnet ef
// dotnet tool install --global dotnet-ef
//         AWARYJNIE: dotnet new tool-manifest
// dotnet ef migrations add Init
// dotnet ef database update   