using Microsoft.EntityFrameworkCore;
using Pagos.Aplicacion.Respositorios;
using Pagos.Aplicacion.Servicios;
using Pagos.Infraestructura.Data;
using Pagos.Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Reistrar DbConext
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PagosDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar repositorios
builder.Services.AddScoped<IPagoRepositorio, PagoRepositorio>();

// Registrar servicios
builder.Services.AddScoped<PagoServicio>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
