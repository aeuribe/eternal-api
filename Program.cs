using eternal_api.Application.Common.Interfaces;
using eternal_api.Infraestructure.Persistence;
using eternal_api.Infrastructure.Persistence;

using eternal_api.WebAPI.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Leer la ConnectionString
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Reemplazar el placeholder con la variable de entorno
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
if (string.IsNullOrEmpty(password))
{
    throw new Exception("La variable de entorno DB_PASSWORD no está definida.");
}
connString = connString.Replace("{DB_PASSWORD}", password);

// Agregar DbContext con PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString));


// Add services to the container.
// Services for Bill
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IHistPriceRepository, HistPriceRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVisitLogRepository, VisitLogRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Mapeo de endpoints
app.MapBillEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 🔧 Endpoints
app.MapProductEndpoints();
app.MapHistPriceEndpoints();
app.MapBillEndpoints();
app.MapCityEndpoints();
app.MapStoreEndpoints();
app.MapUserEndpoints();
app.MapVisitLogEndpoints();

app.Run();
