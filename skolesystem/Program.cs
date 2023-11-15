using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()  // Allow any headers
                                .AllowAnyMethod(); // A
                      });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MySQL database connection
builder.Services.AddDbContext<User_informationDbContext>(
    o => o.UseMySql(builder.Configuration.GetConnectionString("MySQL"), new MySqlServerVersion(new Version(8, 0, 35))));
builder.Services.AddDbContext<SkemaDbContext>(
    o => o.UseMySql(builder.Configuration.GetConnectionString("MySQL"), new MySqlServerVersion(new Version(8, 0, 35))));
builder.Services.AddDbContext<UsersDbContext>(
    o => o.UseMySql(builder.Configuration.GetConnectionString("MySQL"), new MySqlServerVersion(new Version(8, 0, 35))));


// Register your repository
builder.Services.AddScoped<IUser_informationRepository, User_informationRepository>();
builder.Services.AddScoped<ISkemaRepository, SkemaRepository>();

// Register Service
builder.Services.AddScoped<IUser_informationService, User_informationService>();
//builder.Services.AddScoped<ISkemaService, SkemaService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
